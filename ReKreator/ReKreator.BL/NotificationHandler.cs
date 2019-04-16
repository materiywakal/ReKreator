using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReKreator.BL.Constants;
using ReKreator.DAL.Interfaces;
using ReKreator.Domain;
using ReKreator.Domain.Enums;
using ReKreator.Emailing;

namespace ReKreator.BL
{
    public class NotificationHandler
    {
        private IUnitOfWork _dbHandler;
        private ISender _sender;
        private NotificationPeriodDictionary _dictionary;

        public NotificationHandler(IUnitOfWork dbHandler, ISender sender)
        {
            _dbHandler = dbHandler;
            _sender = sender;
            _dictionary = new NotificationPeriodDictionary();
        }

        public async Task NotifyUsersAboutFavoritesEventsAsync()
        {
            var favoriteEvents = await _dbHandler.EventHolding_UserRepository.GetAllAsync(
                o => o.NotificationPeriodsBeforeEvent != NotificationPeriod.None, null, 0, null, o => o.User,
                o => o.EventHolding, o => o.EventHolding.Event);
            if (favoriteEvents == null)
            {
                return;
            }

            foreach (var favoriteEvent in favoriteEvents)
            {
                foreach (NotificationPeriod flag in Enum.GetValues(typeof(NotificationPeriod)))
                {
                    if (flag == NotificationPeriod.None)
                    {
                        continue;
                    }

                    if (favoriteEvent.NotificationPeriodsBeforeEvent.HasFlag(flag))
                    {
                        if ((favoriteEvent.EventHolding.Time - DateTime.Now).TotalHours
                            <= _dictionary.Container[flag])
                        {
                            await SendMessageAboutFavoriteAsync(favoriteEvent.User,
                                favoriteEvent.EventHolding.Event.Title,
                                favoriteEvent.EventHolding.Time);
                            favoriteEvent.NotificationPeriodsBeforeEvent =
                                favoriteEvent.NotificationPeriodsBeforeEvent ^ flag;
                        }
                    }
                }

                _dbHandler.EventHolding_UserRepository.Update(favoriteEvent);
                await _dbHandler.SaveAsync();
            }
        }

        public async Task NotifyUsersAboutNewEventsAsync()
        {
            var users = await _dbHandler.UserRepository.GetAllAsync(
                o => o.UserMailing.MailingPeriod != NoveltyMailingPeriod.None, includes: o => o.UserMailing);
            var events =
                await _dbHandler.EventRepository.GetAllAsync(o => !o.IsRemoved, o => o.OrderBy(e => e.CreationDate),
                    take: 5);
            foreach (var user in users)
            {
                if ((DateTime.Now - user.UserMailing.LasTimeMailed).TotalHours >= (int) user.UserMailing.MailingPeriod)
                {
                    await SendMessageAboutNewEventsAsync(user, events);
                    user.UserMailing.LasTimeMailed = DateTime.Now;
                }

                _dbHandler.UserRepository.Update(user);
                await _dbHandler.SaveAsync();
            }
        }

        private async Task SendMessageAboutFavoriteAsync(User to, string title, DateTime time)
        {
            await _sender.MessageToUserAsync(to, "Favorite Event",
                $"<div>Event {title} will start {time.ToString(DateTimeFormats.ShortDateFormat)} at {time.ToString(DateTimeFormats.ShortTimeFormat)}.</div>");
        }

        private async Task SendMessageAboutNewEventsAsync(User to, IEnumerable<Event> events)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<div>Here is most recent events:</div>");
            foreach (var item in events)
            {
                html.Append($"<div>{item.Title} - Start date: {item.StartDate.ToString(DateTimeFormats.ShortDateFormat)}</div>");
            }

            await _sender.MessageToUserAsync(to, "New events.", html.ToString());
        }
    }
}