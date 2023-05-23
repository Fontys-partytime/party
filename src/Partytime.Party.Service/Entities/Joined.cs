namespace Partytime.Party.Service.Entities
{
    public class Joined
    {
        public Guid Joinedid {get; set;}
        public Guid UserId { get; set; }
        public DateTimeOffset Joineddate { get; set; }
        public DateTimeOffset Accepteddate { get; set; }
        public bool Accepted { get; set; }

        public Guid Partyid { get; set; } // Required foreign key property
        public Party Party { get; set; } = null!; // Required reference navigation to principal
    }
}