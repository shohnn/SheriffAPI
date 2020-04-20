using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Contracts
{
    public interface IBandRepository
    {
        Task<IQueryable<Band>> Get();
        Task<Band> Get(int id);
        Task<Band> Add(Band band);
        Task<BandMember> AddMember(BandMember member);
    }
}