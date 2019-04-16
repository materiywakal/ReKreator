using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Contact
{
    public class ContactViewModel
    {
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
