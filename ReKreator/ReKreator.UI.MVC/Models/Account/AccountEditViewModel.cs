using System.ComponentModel.DataAnnotations;
using ReKreator.DAL.Constants;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountEditViewModel
    {
        [StringLength(UserEntityConstants.FirstNameMaxLength)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [StringLength(UserEntityConstants.LastNameMaxLength)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Требуется ввести почту")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Не правильный Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(UserEntityConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Display(Name = "Период оповещения о новинках")]
        public NoveltyMailingPeriod NoveltyMailingPeriod { get; set; }
    }
}
