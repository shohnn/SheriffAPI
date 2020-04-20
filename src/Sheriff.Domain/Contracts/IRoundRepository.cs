using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Contracts
{
    public interface IRoundRepository
    {
        Task<Round> Get(int id);
        Task<Round> Add(Round round);
        Task<RoundMember> UpdateScoring(RoundMember roundMember);
    }
}