using Partytime.Party.Service.Dtos;
using PartyEntity = Partytime.Party.Service.Entities;

namespace Partytime.Party.Service
{
    public static class Extensions
    {
        public static PartyDto AsDto(this PartyDto party, List<PartyJoinedDto> joinedParty)
        {
            return new PartyDto(party.Id, party.Userid, party.Title, party.Description, party.Starts, party.Ends, party.Location, party.Budget, joinedParty);
        }
    }
}