using Entities = Partytime.Party.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partytime.Party.Service.Repositories
{
    public interface IPartyRepository
    {
        Task<IEnumerable<Entities.Party>>? GetParties();
        Task<Entities.Party> GetPartyById(string id);
        Task<bool> CreateParty();
        Task<bool> UpdateParty();
        Task<bool> DeleteParty();
    }
}
