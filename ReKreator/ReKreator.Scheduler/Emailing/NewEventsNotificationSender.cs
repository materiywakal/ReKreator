using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using ReKreator.BL;

namespace ReKreator.Scheduler.Emailing
{
    public class NewEventsNotificationSender : IJob
    {
        private ILogger<NewEventsNotificationSender> _logger;
        private NotificationHandler _handler;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger = (ILogger<NewEventsNotificationSender>)context.JobDetail.JobDataMap["logger"];
            _handler = (NotificationHandler)context.JobDetail.JobDataMap["notificationHandler"];
            try
            {
                await _handler.NotifyUsersAboutNewEventsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}