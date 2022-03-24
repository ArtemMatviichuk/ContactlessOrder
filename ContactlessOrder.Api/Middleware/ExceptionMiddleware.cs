using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.Api.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _messageTemplate;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _messageTemplate = "HTTP {Domain} {RequestMethod} {StatusCode} {RequestPath}";
            _logger = Log.ForContext<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var domain = httpContext.Request.Host;
            var username = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Username");

            var originalResponseBody = httpContext.Response.Body;

            try
            {
                using (var responseBodyStream = new MemoryStream())
                {
                    httpContext.Response.Body = responseBodyStream;

                    await _next(httpContext);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

                    var statusCode = httpContext.Response.StatusCode;
                    if (statusCode > 399)
                    {
                        var headers = GetHeaders(httpContext);

                        _logger.Write(LogEventLevel.Error, $"\n\n{_messageTemplate}\n\n{username}\n\n{responseBody}\n\n{headers}",
                            domain, httpContext.Request.Method, httpContext.Response.StatusCode,
                            httpContext.Request.Path);
                    }

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(originalResponseBody);
                }
            }
            catch (Exception ex)
            {
                if (httpContext.Response != null)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                var headers = GetHeaders(httpContext);

                _logger.Write(LogEventLevel.Fatal,
                    $"\n\n{_messageTemplate}\n\n{username}\n\n{ex.Message}\n\n{ex.StackTrace}\n\n{headers}",
                    domain, httpContext.Request.Method, httpContext.Response?.StatusCode, httpContext.Request.Path);

                throw;
            }
            finally
            {
                httpContext.Response.Body = originalResponseBody;
            }
        }

        private string GetHeaders(HttpContext httpContext)
        {
            var sb = new StringBuilder();
            var separator = new string('=', 20);

            sb.AppendLine("Request Headers");
            foreach (var (key, value) in httpContext.Request.Headers)
            {
                sb.AppendLine(separator);
                sb.AppendLine($"{key}: {value}");
            }

            if (httpContext.Response != null)
            {
                sb.AppendLine();
                sb.AppendLine("Response Headers");
                foreach (var (key, value) in httpContext.Response.Headers)
                {
                    sb.AppendLine(separator);
                    sb.AppendLine($"{key}: {value}");
                }
            }

            return sb.ToString();
        }
    }
}
