namespace Partytime.Party.Contracts
{
    public record PartyGetById(Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyCreated(Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyUpdated(Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location);
    public record PartyDeleted(Guid id);
}