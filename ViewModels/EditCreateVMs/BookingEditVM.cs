using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.EditCreateVMs
{
    public class BookingEditVM
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Starttid")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [Display(Name = "Bokningsnamn")]
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Telefonnummer")]
        [StringLength(20)]
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Totala bordsplatser")]
        [Range(1.00, 1_000_000.00)]
        [Required]
        public int Seatings { get; set; }
        [Display(Name = "Gästantal")]
        [Range(1.00, 1_000_000.00)]
        [Required]
        public int CustomerAmount { get; set; }
        [Display(Name = "Bordsnummer")]
        [Range(1.00, 1_000_000.00)]
        [Required]
        public int TableNumber { get; set; }
    }
}
