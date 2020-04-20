using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Contracts
{
    public interface IBanditRepository
    {
        Task<IQueryable<Bandit>> Get();
        Task<Bandit> Get(int id);
        Task<Bandit> FindByEmail(string email);
        Task<Bandit> Add(Bandit bandit);
    }
}