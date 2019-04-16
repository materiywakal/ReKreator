using ReKreator.Domain.Enums;
using System;

namespace ReKreator.Domain
{
    public class UserMailing
    {
        public long UserMailingId { get; set; }

        public NoveltyMailingPeriod MailingPeriod { get; set; }
        public DateTime LasTimeMailed { get; set; }

        public long UserId { get; set; }
    }
}
