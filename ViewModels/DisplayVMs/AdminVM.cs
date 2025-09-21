using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.ViewModels.DisplayVMs
{
    public class AdminVM
    {
        public int Id { get; set; }
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }
    }
}
