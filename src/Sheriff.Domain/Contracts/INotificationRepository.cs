using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Contracts
{
    public interface INotificationRepository
    {
        Task<IQueryable<Notification>> GetFor(int banditId);
        Task<Notification> Add(Notification notification);
        Task<Notification> Remove(int id);
    }
}