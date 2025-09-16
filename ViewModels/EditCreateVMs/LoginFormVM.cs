using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.EditCreateVMs
{
    public class LoginFormVM
    {
        [Required(ErrorMessage = "Du måste skriva in Användarnamn")]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Du måste skriva in Lösenord")]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}
