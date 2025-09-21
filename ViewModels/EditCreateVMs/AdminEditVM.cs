using AdvFullstack_Labb2.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.EditCreateVMs
{
    public class AdminEditVM
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required( ErrorMessage = "Användarnamn är obligatoriskt")]
        [Display(Name = "Användarnamn")]
        [MinLength(8, ErrorMessage = "Användarnamn måste vara minst 10 tecken.")]
        [StringLength(50, ErrorMessage = "Användarnamn måste vara max 50 tecken.")]
        public string Username { get; set; }

        // For password confirmation step
        [Display(Name = "Nuvarande Lösenord")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Lösenord måste vara minst 10 tecken.")]
        [StringLength(200)]
        public string CurrentPassword { get; set; }

        // For Edit Step

        [Display(Name = "Nytt Lösenord (Lämna blankt om du inte vill byta)")]
        [DataType(DataType.Password)]
        [OptionalMinLength(10, ErrorMessage = "Lösenord måste vara minst 10 tecken.")]
        [StringLength(200)]
        public string? NewPassword { get; set; }

        [Display(Name = "Bekräfta Nytt Lösenord")]
        [DataType(DataType.Password)]
        [StringLength(200)]

        [Compare("NewPassword", ErrorMessage = "Lösenorden matchar inte.")]
        public string? ConfirmNewPassword { get; set; }
        public EditStep Step { get; set; } = EditStep.PasswordConfirm;
    }

    public enum EditStep
    {
        PasswordConfirm,
        EditForm
    }
}
