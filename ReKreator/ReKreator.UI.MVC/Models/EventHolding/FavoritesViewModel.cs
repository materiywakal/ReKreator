using System.Collections.Generic;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public class FavoritesViewModel
    {
        public ICollection<EventHoldingViewModel> EventHoldings { get; set; }
        public EventHoldingFilterViewModel EventHoldingFilter { get; set; }
    }
}
