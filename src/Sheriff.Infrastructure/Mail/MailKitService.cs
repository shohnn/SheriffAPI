using System;
using MimeKit;
using System.Linq;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Sheriff.Infrastructure.Mail
{
    public class MailKitService : IEmailService
    {
        private IEmailConfiguration configuration;

        public MailKitService(IEmailConfiguration emailConfiguration)
        {
            configuration = emailConfiguration;
        }

        public async Task Send(EmailMessage emailMessage)
        {
            var toAddresses = emailMessage.ToAddresses
                .Select(x => new MailboxAddress(x.DisplayName, x.Address));

            var fromAddress = new MailboxAddress(configuration.SmtpUsername);

            var message = new MimeMessage();

            message.To.AddRange(toAddresses);
            message.From.Add(fromAddress);
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(configuration.SmtpServer, configuration.SmtpPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                await emailClient.AuthenticateAsync(configuration.SmtpUsername, configuration.SmtpPassword);

                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(true);
            }
        }
    }
}