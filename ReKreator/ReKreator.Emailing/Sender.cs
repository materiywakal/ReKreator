using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using ReKreator.Domain;

namespace ReKreator.Emailing
{
    public class Sender : ISender
    {
        private EmailAddress Email;
        private SendGridClient Client;

        private const string _layoutHtml =
            "<div>Best regards, Re Kreator.</div><div><img src=\"https://cdn1.imggmi.com/uploads/2019/4/10/fd8a13b5d4b21c7b6684607681896bd9-full.png\" style=\"width:200px !important; height:200px !important;\"/></div></div>";

        public Sender(string apiKey, string email)
        {
            Email = new EmailAddress(email, "Re Kreator");
            Client = new SendGridClient(apiKey);
        }

        public async Task MessageToUserAsync(User user, string subject, string html)
        {
            var to = new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}");
            var msg = MailHelper.CreateSingleEmail(Email, to, subject, string.Empty,"<div>" + html + _layoutHtml);
            var response = await Client.SendEmailAsync(msg);
        }

        public async Task MessageToUserAsync(IEnumerable<User> users, string subject, string html)
        {
            foreach (var user in users)
            {
                await Task.Run(() => MessageToUserAsync(user, subject, html));
            }
        }

        public async Task ContactMessageAsync(string username, string subject, string text)
        {
            await Task.Run(
                () => MessageToUserAsync(new User {Email = Email.Email, FirstName = username}, subject, text));
        }
    }
}