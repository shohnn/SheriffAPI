using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Notifications
{
    public interface ISender
    {
        Task Send(Message message, Bandit to);
    }
}