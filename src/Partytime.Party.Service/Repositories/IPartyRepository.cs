using Entities = Partytime.Party.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partytime.Party.Service.Repositories
{
    public interface IPartyRepository
    {
        Task<List<Entities.Party>> GetPartiesByUserId(Guid userId);
        Task<Entities.Party?> GetPartyById(Guid id);
        Task<Entities.Party?> AddJoinedToParty(Guid partyId, Entities.Joined joined);
        Task<Entities.Party?> CheckIfJoinedExistsInParty(Guid partyId, Guid userId);
        Task<Entities.Party> CreateParty(Entities.Party party);
        Task<Entities.Party> UpdateParty(Guid id, Entities.Party party);
        Task<bool> DeleteParty(Guid id);
    }
}
