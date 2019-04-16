using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Account
{
    public class AccountProfileViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        [Display(Name = "Period of novelty mailing")]
        public NoveltyMailingPeriod NoveltyMailingPeriod { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }
    }
}
