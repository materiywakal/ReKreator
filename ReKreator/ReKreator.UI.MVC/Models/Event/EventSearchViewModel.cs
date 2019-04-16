using System;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Event
{
    public class EventSearchViewModel
    {
        public long EventId { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public EventGenre Genres { get; set; }
        public string SourceUrl { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
