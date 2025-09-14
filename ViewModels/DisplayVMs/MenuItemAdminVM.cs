using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.DisplayVMs
{
    public class MenuItemAdminVM
    {
        public int Id { get; set; }
        [Display(Name = "Varunamn")]
        public string Name { get; set; }
        [Display(Name = "Pris")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        [Display(Name = "Populär?")]
        public bool IsPopular { get; set; }
        [Display(Name = "Bild Url")]
        public string ImageUrl { get; set; }
    }
}
