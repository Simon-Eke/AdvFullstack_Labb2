namespace AdvFullstack_Labb2.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public int CustomerAmount { get; set; }
        public DateTime StartTime { get; set; }
    }
}
