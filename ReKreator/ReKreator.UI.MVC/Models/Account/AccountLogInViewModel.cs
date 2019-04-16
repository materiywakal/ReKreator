using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountLogInViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Login (Username)")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
