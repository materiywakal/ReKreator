using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountProfileViewModel
    {
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Роль")]
        public ICollection<string> Roles { get; set; }

        [Display(Name = "Уведомления о новинках")]
        public NoveltyMailingPeriod NoveltyMailingPeriod { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }
    }
}
