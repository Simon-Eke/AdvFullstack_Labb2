namespace AdvFullstack_Labb2.Models
{
    public class BookingWithDetails
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Table Table { get; set; }
        public int CustomerAmount { get; set; }
        public DateTime StartTime { get; set; }
    }
}
