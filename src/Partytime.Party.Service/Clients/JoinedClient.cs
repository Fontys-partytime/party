using Partytime.Party.Service.Dtos;

namespace Partytime.Party.Service.Clients
{
    public class JoinedClient
    {
        private readonly HttpClient httpClient;
        public JoinedClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // Searches for every joined person in the joined microservice
        // by filtering through PartyId
        public async Task<List<PartyJoinedDto>> GetPartyJoinedByPartyAsync(Guid partyId)
        {
            var partyJoined = await httpClient.GetFromJsonAsync<List<PartyJoinedDto>>("/joined/" + partyId);
            
            return partyJoined;
        }
    }
}