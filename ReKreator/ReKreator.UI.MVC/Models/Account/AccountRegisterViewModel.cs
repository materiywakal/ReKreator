using System.ComponentModel.DataAnnotations;
using ReKreator.DAL.Constants;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Требуется ввести Логин")]
        [StringLength(UserEntityConstants.UsernameMaxLength, MinimumLength = UserEntityConstants.UsernameMinLength)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Требуется ввести Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Неверный Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(UserEntityConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется ввести пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Требуется подтверждение пароля")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirmation { get; set; }

        [StringLength(UserEntityConstants.FirstNameMaxLength)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [StringLength(UserEntityConstants.LastNameMaxLength)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
