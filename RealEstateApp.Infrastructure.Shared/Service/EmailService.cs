

using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Settings;


namespace RealEstateApp.Infrastructure.Shared.Service
{
    public class EmailService : IEmailService
    {
        public MailSettings MailSettings { get; }
        

        public EmailService(IOptions<MailSettings> mailSettings) 
        { 
            MailSettings = mailSettings.Value;
        }
        public async Task SendAsync(EmailRequest request)
        {
            try 
            {
                //create message
                var email = new MimeMessage();
                email.Sender= MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject= request.Subject;
                var builder= new BodyBuilder();
                email.Body= builder.ToMessageBody();
                using var smtp= new SmtpClient();
                smtp.Connect(MailSettings.SmtpHost, MailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                

            }
            catch(Exception ex) { }
        }
    }
}
