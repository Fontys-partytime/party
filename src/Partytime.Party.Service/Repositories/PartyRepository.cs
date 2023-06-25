using Npgsql;
using Partytime.Party.Service.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Partytime.Party.Service.Dtos;
using MassTransit.JobService;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Entities = Partytime.Party.Service.Entities;

namespace Partytime.Party.Service.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly PartyContext _context;

        public PartyRepository(PartyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Entities.Party>> GetPartiesByUserId(Guid userId)
        {
            List<Entities.Party> parties = await _context.Parties.Where(party => party.Userid == userId).ToListAsync();

            //foreach (Entities.Party party in parties)
            //{
            //    List<Entities.Joined> joined = await _context.Joined.Where(joined => joined.Partyid == party.Id).ToListAsync();
            //        party.Joined = joined;
            //}

            return parties;
        }

        public async Task<Entities.Party?> GetPartyById(Guid id)
        {
            var party = await _context.Parties.FindAsync(id);

            //List<Entities.Joined> joined = await _context.Joined.Where(joined => joined.Partyid == id).ToListAsync();
            //party.Joined = joined;

            return party;
        }

        // This is needed for walking skeleton, checks if joined already exists
        //public async Task<Entities.Party?> CheckIfJoinedExistsInParty(Guid partyId, Guid userId)
        //{
        //    // Checks if there is any joined user in the joined list in the party entity
        //    var party = await _context.Parties.Where(party => party.Joined.Any(joined => joined.Userid == userId)).FirstOrDefaultAsync();
        //
        //    return party;
        //}

        public async Task<Entities.Party> CreateParty(Entities.Party party)
        {
            await _context.Parties.AddAsync(party);
            await _context.SaveChangesAsync();

            return party;
        }

        public async Task<Entities.Party> UpdateParty(Guid id, Entities.Party party)
        {
            var partyFound = _context.Parties.FirstOrDefault(prty => prty.Id == id);

            if(partyFound != null)
            {
                partyFound.Title = party.Title;
                partyFound.Description = party.Description;
                partyFound.Starts = party.Starts;
                partyFound.Ends = party.Ends;
                partyFound.Amount = party.Amount;
                partyFound.Paymentlink = party.Paymentlink;
                partyFound.Linkexperation = party.Linkexperation;

                await _context.SaveChangesAsync();
                return partyFound;
            }
            
            return party;
        }

        public async Task<bool> DeleteParty(Guid id)
        {
            var partyToDelete = _context.Parties.SingleOrDefault(party => party.Id == id);
            
            if (partyToDelete != null) {
                _context.Parties.Remove(partyToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}