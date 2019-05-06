using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Contact
{
    public class ContactViewModel
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Требуется указать тему")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Требуется ввести сообщение")]
        [Display(Name = "Сообщение")]
        public string Text { get; set; }
    }
}
