using System.ComponentModel.DataAnnotations;
using ReKreator.DAL.Constants;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(UserEntityConstants.UsernameMaxLength, MinimumLength = UserEntityConstants.UsernameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [StringLength(UserEntityConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirmation { get; set; }

        [StringLength(UserEntityConstants.FirstNameMaxLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(UserEntityConstants.LastNameMaxLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}
