using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.DisplayVMs
{
    public class MenuItemPublicVM
    {
        [Display(Name = "Varunamn")]
        public string Name { get; set; }
        [Display(Name = "Pris")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        [Display(Name = "Bild")]
        public string ImageUrl { get; set; }
    }
}
