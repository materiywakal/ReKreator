using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountPasswordEditVIewModel
    {
        [Required(ErrorMessage = "Old password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "New password confirmation is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string NewPasswordConfirmation { get; set; }
    }
}
