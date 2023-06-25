using System.ComponentModel.DataAnnotations.Schema;

namespace Partytime.Party.Service.Entities
{
    [Table("party")]
    public class Party
    {
        public Guid Id { get; set; }
        public Guid Userid { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset Starts { get; set; }
        public DateTimeOffset Ends { get; set; }
        public decimal? Amount { get; set; }
        public string? Paymentlink { get; set; }
        public DateTimeOffset Linkexperation { get; set; }

        //public List<Joined> Joined { get; set; }

        //public Party() 
        //{ 
        //    Joined = new List<Joined>();
        //}
    }
}