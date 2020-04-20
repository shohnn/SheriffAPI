using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Contracts
{
    public interface IEmailService
    {
        Task Send(EmailMessage message);
    }
}