using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Notifications
{
    class Mailer : ISender
    {
        private IEmailService emailService;

        public Mailer(IEmailService emailService)
        {
            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));

            this.emailService = emailService;
        }

        public async Task Send(Message message, Bandit to)
        {
            var email = new EmailMessage
            {
                ToAddresses = new List<EmailAddress> { to.Email },
                Subject = message.Title,
                Content = message.Body
            };

            await emailService.Send(email);
        }
    }
}