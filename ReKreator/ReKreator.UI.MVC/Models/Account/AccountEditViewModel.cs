using System.ComponentModel.DataAnnotations;
using ReKreator.DAL.Constants;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountEditViewModel
    {
        [StringLength(UserEntityConstants.FirstNameMaxLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(UserEntityConstants.LastNameMaxLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [StringLength(UserEntityConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Display(Name = "Period of novelty mailing")]
        public NoveltyMailingPeriod NoveltyMailingPeriod { get; set; }
    }
}
