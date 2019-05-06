using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountLogInViewModel
    {
        [Required(ErrorMessage = "Требуется ввести Email")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Требуется ввести пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
