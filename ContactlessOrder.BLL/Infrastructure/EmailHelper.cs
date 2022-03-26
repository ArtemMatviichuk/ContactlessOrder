using ContactlessOrder.Common.Constants;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Infrastructure
{
    public class EmailHelper
    {
        private readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmEmail(string toEmail, string fullName, string token)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_configuration[AppConstants.FromEmail]));

            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = "Підтвердіть адресу електронної пошти | Contactless Order";

            var logoPath = _configuration[AppConstants.LogoPath];
            var builder = new BodyBuilder();

            var image = builder.LinkedResources.Add(logoPath);
            image.ContentId = MimeUtils.GenerateMessageId();
            var contentId = image.ContentId;
            builder.HtmlBody = $@"
Привіт {fullName}, <br>
<br>
Дякую за реєстрацію! Для підтвердження електронної пошти <a href=""{_configuration[AppConstants.CorsOrigin]}/auth/confirm-email?token={token}"">натисніть на посилання</a> <br>
<br>
Всього найкращого, <br>
&copy; Contactless Order<br>
<img style=""width:600px"" src=""cid:{contentId}""/><br>
";

            emailMessage.Body = builder.ToMessageBody();

            var smtpUserName = _configuration[AppConstants.SMTPMailServerUsername];
            var smtpPassword = _configuration[AppConstants.SMTPMailServerPassword];
            var port = int.Parse(_configuration[AppConstants.SMTPMailServerPort]);
            var host = _configuration[AppConstants.SMTPMailServerServer];
            var option = int.Parse(_configuration[AppConstants.SMTPMailUseSSL]);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(host, port, (SecureSocketOptions)option);

                if (!string.IsNullOrWhiteSpace(smtpUserName) && !string.IsNullOrWhiteSpace(smtpPassword))
                {
                    await client.AuthenticateAsync(smtpUserName, smtpPassword);
                }

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(false);
            }
        }
    }
}
