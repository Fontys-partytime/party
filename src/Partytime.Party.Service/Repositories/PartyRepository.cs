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

namespace Partytime.Party.Service.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly IConfiguration _configuration;

        public PartyRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
        }

        public async Task<List<Entities.Party>>? GetParties()
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            List<Entities.Party> parties = (List<Entities.Party>)await connection.QueryAsync<Entities.Party>("SELECT * FROM party");

            return parties;
        }

        public async Task<Entities.Party> GetPartyById(Guid id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            Entities.Party party = await connection.QueryFirstOrDefaultAsync<Entities.Party>("SELECT * FROM party WHERE Id=@id::uuid", new { id });

            return party;
        }

        public async Task<Entities.Party> CreateParty(Entities.Party party)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            
            var query = @"INSERT INTO party (UserId,Title,Description,Starts,Ends,Location,Budget) VALUES (@party.UserId,@party.Title,@party.Description,@party.Starts,@party.Ends,@party.Location,@party.Budget)";
            var partyCreated = await connection.ExecuteAsync(query);

            return party;
        }

        public async Task<Entities.Party> UpdateParty(Entities.Party party)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            // @party.Id,@party.UserId,@party.Title,@party.Description,@party.Starts,@party.Ends,@party.Budget
            var query = @"update party 
                set Id = @party.Id,
                set UserId = @party.UserId,
                set Title = @party.Title,
                set Description = @party.Description,
                set Starts = @party.Starts,
                set Ends = @party.Ends,
                set Location = @party.Location,
                set Budget = @party.Budget";
            var partyCreated = await connection.ExecuteAsync(query);

            return party;
        }

        public async Task<bool> DeleteParty(Guid id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = @"DELETE FROM party WHERE Id = @id";

            var deleted = await connection.ExecuteAsync(query);

            if (deleted == 0)
                return false;
            return true;
        }
    }
}