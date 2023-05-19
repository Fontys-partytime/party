namespace Partytime.Party.Service.Entities
{
    public class Party
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset Starts { get; set; }
        public DateTimeOffset Ends { get; set; }
        public string? Location { get; set; }
        public decimal? Budget { get; set; }

        public Party() 
        { 
        
        }
    }
}