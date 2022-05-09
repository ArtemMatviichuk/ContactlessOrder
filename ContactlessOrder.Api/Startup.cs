using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ContactlessOrder.Api.Hubs;
using ContactlessOrder.Api.JsonConverters;
using ContactlessOrder.Api.Middleware;
using ContactlessOrder.Api.Validators;
using ContactlessOrder.BLL.Infrastructure;
using ContactlessOrder.BLL.Infrastructure.MappingProfiles;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.BLL.Services;
using ContactlessOrder.BLL.HubConnections.Hubs;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Interfaces;
using ContactlessOrder.DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.Email;

namespace ContactlessOrder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new StringConverter());
                    opt.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });

            services.AddDbContext<ContactlessOrderContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("COLocal")));

            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICateringService, CateringService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICateringRepository, CateringRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();

            services.AddTransient<EmailHelper>();
            services.AddTransient<FileHelper>();

            services.AddAutoMapper(typeof(Startup), typeof(UserMappingProfile));

            services.AddTransient<IValidator<UserDto>, UserValidation>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var error = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).FirstOrDefault();
                    var result = new
                    {
                        Message = error
                    };
                    return new BadRequestObjectResult(result);
                };
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(Configuration[AppConstants.CorsOrigin])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            
                            return Task.CompletedTask;
                        }
                    };
                });

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.File(Configuration.GetValue<string>(AppConstants.LogPath))
                .WriteTo.Logger(l => l
                    .Filter.ByIncludingOnly(Matching.FromSource<ExceptionMiddleware>())
                    .WriteTo.Email(new EmailConnectionInfo()
                    {
                        FromEmail = Configuration.GetValue<string>(AppConstants.ErrorFromEmail),
                        ToEmail = Configuration.GetValue<string>(AppConstants.ErrorToEmail),
                        EmailSubject = Configuration.GetValue<string>(AppConstants.ErrorMailSubject) + " {Domain}",
                        NetworkCredentials = new NetworkCredential(
                            Configuration.GetValue<string>(AppConstants.SmtpErrorUserName),
                            Configuration.GetValue<string>(AppConstants.SmtpErrorPassword)),
                        Port = Configuration.GetValue<int>(AppConstants.SmtpErrorPort),
                        MailServer = Configuration.GetValue<string>(AppConstants.SmtpErrorServer),
                    }))
                .CreateLogger();

            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddSignalR(opt =>
            {
                opt.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
                opt.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ContactlessOrderContext>().Database.Migrate();

                (Configuration as IConfigurationRoot)?.Reload();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(c =>
            {
                c.MapControllers();
                c.MapHub<OrdersHub>("/orders", opt => opt.Transports = HttpTransportType.WebSockets);
            });
        }
    }
}
