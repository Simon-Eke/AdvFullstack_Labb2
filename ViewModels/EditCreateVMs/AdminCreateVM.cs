using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.EditCreateVMs
{
    public class AdminCreateVM
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        [Display(Name = "Användarnamn")]
        [MinLength(8, ErrorMessage = "Användarnamn måste vara minst 10 tecken.")]
        [StringLength(50, ErrorMessage = "Användarnamn måste vara max 50 tecken.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Lösenord måste vara minst 10 tecken.")]
        [StringLength(200)]
        public string Password { get; set; }
    }
}
