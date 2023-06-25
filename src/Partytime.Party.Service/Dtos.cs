using System.ComponentModel.DataAnnotations;

namespace Partytime.Party.Service.Dtos // Abstracts the entity and also the properties the entity has in the future
{
    public record PartyDto(Guid Id, Guid Userid, string? Title, string? Description, DateTimeOffset Starts, DateTimeOffset Ends, string? Location, [Range(0, 1000)] decimal? Amountstring, string? Paymentlink, DateTimeOffset Linkexperation);
    
    public record CreatePartyDto([Required] Guid Userid, [Required] string? Title, string? Description, DateTimeOffset Starts, DateTimeOffset Ends, string? Location, [Range(0, 1000)] decimal? Amount, string? Paymentlink, DateTimeOffset Linkexperation);

    public record UpdatePartyDto(string? title, string? description, DateTimeOffset starts, DateTimeOffset ends, [Range(0, 1000)] decimal? amount, string? paymentlink, DateTimeOffset linkexperation);
}