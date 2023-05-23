using Npgsql;
using Dapper;
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

namespace Partytime.Party.Service.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly PartyContext _context;

        public PartyRepository(PartyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Entities.Party>> GetParties()
        {
            List<Entities.Party> parties = await _context.Parties.ToListAsync();

            return parties;
        }

        public async Task<Entities.Party?> GetPartyById(Guid id)
        {
            var party = await _context.Parties.FindAsync(id);

            return party;
        }

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
                partyFound.Userid = party.Userid;
                partyFound.Title = party.Title;
                partyFound.Description = party.Description;
                partyFound.Starts = party.Starts;
                partyFound.Ends = party.Ends;
                partyFound.Location = party.Location;
                partyFound.Budget = party.Budget;

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