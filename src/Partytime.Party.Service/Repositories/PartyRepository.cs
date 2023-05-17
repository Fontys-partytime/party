using Npgsql;
using Dapper;
using Partytime.Party.Service.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partytime.Party.Service.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly IConfiguration _configuration;
        private static string ConnectionString = "Server=localhost;Port=5432;Database=party;User Id=tommie;Password=Rfec7Fo81YchjyUu;";

        public PartyRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
        }

        public async Task<IEnumerable<Entities.Party>>? GetParties()
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            IEnumerable<Entities.Party> parties = await connection.QueryAsync<Entities.Party>("SELECT * FROM Party");

            return parties;
        }

        public async Task<Entities.Party> GetPartyById(string id)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            Entities.Party party = await connection.QueryFirstAsync<Entities.Party>("SELECT * FROM Party WHERE Id = @id");

            return party;
        }

        public Task<bool> CreateParty()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateParty()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteParty()
        {
            throw new NotImplementedException();
        }
    }
}