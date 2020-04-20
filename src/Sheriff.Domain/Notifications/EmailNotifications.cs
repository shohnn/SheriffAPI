using System;
using Sheriff.Domain.Contracts;

namespace Sheriff.Domain.Notifications
{
    public class EmailNotifications : Notifications
    {
        private IEmailService emailService;

        public EmailNotifications(IEmailService emailService)
        {
            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));

            this.emailService = emailService;
        }

        protected override ISender CreateSender()
        {
            return new Mailer(emailService);
        }
    }
}