using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountPasswordEditVIewModel
    {
        [Required(ErrorMessage = "Требуется ввести старый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Требуется ввести новый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Требуется подтвердить пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string NewPasswordConfirmation { get; set; }
    }
}
