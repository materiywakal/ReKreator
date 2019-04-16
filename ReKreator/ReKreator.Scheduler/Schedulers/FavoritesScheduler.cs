using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using ReKreator.BL;
using ReKreator.Scheduler.Emailing;

namespace ReKreator.Scheduler.Schedulers
{
    public class FavoritesScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<FavoritesNotificationSender>().Build();
            job.JobDataMap["logger"] = Program.ServiceProvider.GetRequiredService<ILogger<FavoritesNotificationSender>>();
            job.JobDataMap["notificationHandler"] = Program.ServiceProvider.GetRequiredService<NotificationHandler>();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("FavoritesTrigger", "Emailers")
                .WithCronSchedule("0/1 0 9 ? * * *")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}