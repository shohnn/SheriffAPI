using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Models;    

namespace Sheriff.Domain.Contracts
{    
    public interface IInvitationRepository
    {
        Task<Invitation> Get(int id);
        Task<IQueryable<Invitation>> GetFor(int banditId);
        Task<Invitation> Add(Invitation notification);
        Task<Invitation> Remove(int id);
    }
}