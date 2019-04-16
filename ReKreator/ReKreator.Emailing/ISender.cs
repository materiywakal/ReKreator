using System.Collections.Generic;
using System.Threading.Tasks;
using ReKreator.Domain;

namespace ReKreator.Emailing
{
    public interface ISender
    {
        Task MessageToUserAsync(User user, string subject, string html);
        Task MessageToUserAsync(IEnumerable<User> users, string subject, string html);
        Task ContactMessageAsync(string username, string subject, string text);
    }
}
