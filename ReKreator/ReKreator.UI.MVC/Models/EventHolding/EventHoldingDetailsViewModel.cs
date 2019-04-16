using System;
using System.ComponentModel.DataAnnotations;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public class EventHoldingDetailsViewModel
    {
        public long EventHoldingId { get; set; }
        
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public EventGenre Genres { get; set; }
        public string PosterUrl { get; set; }
        public string SourceUrl { get; set; }
        public string Place { get; set; }

        [Display(Name = "Notify before event")]
        public NotificationPeriodViewModel NotificationsPeriod { get; set; } 

    }
}
