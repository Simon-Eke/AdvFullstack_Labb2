using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.DisplayVMs
{
    public class BookingVM
    {
        public int Id { get; set; }
        [Display(Name = "Starttid")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [Display(Name = "Bokningsnamn")]
        public string Name { get; set; }
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Totala bordsplatser")]
        public int Seatings { get; set; }
        [Display(Name = "Gästantal")]
        public int CustomerAmount { get; set; }
        [Display(Name = "Bordsnummer")]
        public int TableNumber { get; set; }
    }
}
