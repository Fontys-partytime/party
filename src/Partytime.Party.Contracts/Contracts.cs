namespace Partytime.Party.Contracts
{
    //public record Joined()
    public record PartyGetById(Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyCreated(Guid id, Guid UserId, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyUpdated(Guid id, Guid UserId, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyDeleted(Guid id);
}