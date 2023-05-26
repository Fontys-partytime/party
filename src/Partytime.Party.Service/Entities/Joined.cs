using Microsoft.EntityFrameworkCore;

namespace Partytime.Party.Service.Entities
{
    public class Joined
    {
        public Guid Id { get; set; }
        public Guid Partyid { get; set; }
        public Party Party { get; set;}

        public Guid Userid { get; set; }
        public string? Username { get; set; }

        public bool? Accepted { get; set; } // If null, the request hasn't been answered yet

    }
}