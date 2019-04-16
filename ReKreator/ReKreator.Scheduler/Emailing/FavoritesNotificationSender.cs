using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using ReKreator.BL;

namespace ReKreator.Scheduler.Emailing
{
    public class FavoritesNotificationSender : IJob
    {
        private ILogger<FavoritesNotificationSender> _logger;
        private NotificationHandler _handler;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger = (ILogger<FavoritesNotificationSender>)context.JobDetail.JobDataMap["logger"];
            _handler = (NotificationHandler)context.JobDetail.JobDataMap["notificationHandler"];
            try
            {
                await _handler.NotifyUsersAboutFavoritesEventsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}