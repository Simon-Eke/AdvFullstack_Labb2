using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.EditCreateVMs
{
    public class MenuItemEditVM
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Menu Item Name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(1.00, 1_000_000.00)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Popular?")]
        public bool IsPopular { get; set; }
        [Display(Name = "Image Url")]
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
    }
}
