using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReKreator.DAL.Interfaces;
using ReKreator.Domain;

namespace ReKreator.BL.Services
{
    public class ParsedDataHandler
    {
        private IUnitOfWork _dbHandler;

        public ParsedDataHandler(IUnitOfWork dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public async Task SaveAsync(ParsingModel container)
        {
            await SaveEventsAsync(container.Events);
            await SavePlacesAsync(container.EventPlaces);
            await SaveEventHoldingsAsync(container.EventHoldings);
            await _dbHandler.SaveAsync();
            Console.Write("  ~  This part of work is ended.\n");
        }

        private async Task SaveEventsAsync(ICollection<Event> container)
        {
            foreach (var item in container)
            {
                var dbItem = await _dbHandler.EventRepository.GetAsync(o =>
                    o.SourceUrl.Equals(item.SourceUrl, StringComparison.CurrentCulture));               
                if (dbItem == null)
                {
                    item.EventsHoldings = null;
                    _dbHandler.EventRepository.Create(item);
                }
                else
                {
                    dbItem.Title = item.Title;
                    dbItem.ExpiryDate = item.ExpiryDate;
                    dbItem.EventsHoldings = null;
                    dbItem.Genres = item.Genres;
                    dbItem.PosterUrl = item.PosterUrl;
                    dbItem.Type = item.Type;
                    dbItem.Description = item.Description;
                    if (item.StartDate < dbItem.StartDate)
                    {
                        dbItem.StartDate = item.StartDate;
                    }

                    _dbHandler.EventRepository.Update(dbItem);
                }
            }
        }

        private async Task SavePlacesAsync(ICollection<EventPlace> container)
        {
            foreach (var item in container)
            {
                EventPlace dbItem = await _dbHandler.EventPlaceRepository.GetAsync(o =>
                    o.Title.Equals(item.Title, StringComparison.CurrentCulture));
                if (dbItem == null)
                {
                    item.EventsHoldings = null;
                    _dbHandler.EventPlaceRepository.Create(item);
                }
            }
        }

        private async Task SaveEventHoldingsAsync(ICollection<EventHolding> container)
        {
            foreach (var item in container)
            {
                item.Event = await _dbHandler.EventRepository.GetAsync(o => o.SourceUrl == item.Event.SourceUrl) ?? item.Event;
                item.EventPlace = await _dbHandler.EventPlaceRepository.GetAsync(o => o.Title == item.EventPlace.Title) ?? item.EventPlace;

                var dbItem = await _dbHandler.EventHoldingRepository.GetAsync(o =>
                    o.Event == item.Event && o.EventPlace == item.EventPlace && o.Time == item.Time);
                if (dbItem == null)
                {
                    _dbHandler.EventHoldingRepository.Create(item);
                }
            }
        }
    }
}