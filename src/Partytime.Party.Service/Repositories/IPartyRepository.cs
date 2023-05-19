using Entities = Partytime.Party.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partytime.Party.Service.Repositories
{
    public interface IPartyRepository
    {
        Task<List<Entities.Party>>? GetParties();
        Task<Entities.Party> GetPartyById(Guid id);
        Task<Entities.Party> CreateParty(Entities.Party party);
        Task<Entities.Party> UpdateParty(Entities.Party party);
        Task<bool> DeleteParty(Guid id);
    }
}
