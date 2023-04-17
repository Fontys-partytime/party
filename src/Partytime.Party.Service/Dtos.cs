using System.ComponentModel.DataAnnotations;

namespace Partytime.Party.Service.Dtos
{
    public record PartyDto(Guid Id, Guid UserId, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location, decimal Budget, DateTimeOffset CreatedDate);
    
    public record CreatePartyDto([Required] Guid UserId, [Required] string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location, [Range(0, 1000)] decimal Budget);

    public record UpdatePartyDto([Required] string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location, [Range(0, 1000)] decimal Budget);

    // Needed to get joined in client communication
    public record PartyJoinedDto(Guid Id, Guid UserId, DateTimeOffset JoinedDate, DateTimeOffset AcceptedDate, bool Accepted);
}