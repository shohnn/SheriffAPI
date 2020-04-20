using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;
using Sheriff.Domain.Models;

namespace Sheriff.Infrastructure.Repositories
{
    public class AsyncEFNotificationRepository : INotificationRepository
    {
        private SheriffContext dbContext;

        public AsyncEFNotificationRepository(SheriffContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<Notification> Add(Notification notification)
        {
            var bandit = await dbContext
                .Set<Entities.Bandit>()
                .FindAsync(notification.Bandit.Id);

            var dbNotif = new Entities.Notification
            {
                Bandit = bandit,
                Title = notification.Title,
                Body = notification.Body
            };

            await dbContext
                .Set<Entities.Notification>()
                .AddAsync(dbNotif);

            await dbContext.SaveChangesAsync();

            return notification;
        }

        public async Task<IQueryable<Notification>> GetFor(int banditId)
        {
            var dbBandit = await dbContext
                .Set<Entities.Bandit>()
                .Include(b => b.Notifications)
                .FirstOrDefaultAsync(b => b.Id == banditId);

            if (dbBandit == null)
                throw new NotFoundException("Bandit", banditId);

            var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);

            return dbBandit.Notifications
                .Select(n => {
                    var notif = Notification
                        .For(n.Id, bandit, n.Title, n.Body);

                    return notif;
                })
                .AsQueryable();
        }

        public async Task<Notification> Remove(int id)
        {
            var notification = await dbContext
                .Set<Entities.Notification>()
                .Include(n => n.Bandit)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
                return null;

            var bandit = Bandit.Create(notification.Bandit.Id,
                                       notification.Bandit.Name, 
                                       notification.Bandit.Email);

            var notif = Notification.For(
                notification.Id,
                bandit,
                notification.Title,
                notification.Body);

            dbContext
                .Set<Entities.Notification>()
                .Remove(notification);

            await dbContext.SaveChangesAsync();

            return notif;
        }
    }
}