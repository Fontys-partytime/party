using System.ComponentModel.DataAnnotations;

namespace Partytime.Party.Service.Dtos // Abstracts the entity and also the properties the entity has in the future
{
    // PartyJoinedDto is data that's stored seperately, so it should only be added as parameter here
    // By adding nullable and a default value it becomes an optional parameter
    public record PartyDto(Guid Id, Guid Userid, string? Title, string? Description, DateTimeOffset Starts, DateTimeOffset Ends, string? Location, decimal? Budget, List<PartyJoinedDto>? JoinedParty = null);
    
    public record CreatePartyDto([Required] Guid Userid, [Required] string? Title, string? Description, DateTimeOffset Starts, DateTimeOffset Ends, string? Location, [Range(0, 1000)] decimal? Budget);

    public record UpdatePartyDto(Guid Userid, string? Title, string? Description, DateTimeOffset Starts, DateTimeOffset Ends, string? Location, [Range(0, 1000)] decimal? Budget, List<PartyJoinedDto>? JoinedParty = null);

    // Needed to get joined in client communication
    public record PartyJoinedDto(Guid Id, Guid Userid, DateTimeOffset JoinedDate, DateTimeOffset AcceptedDate, bool Accepted);
}