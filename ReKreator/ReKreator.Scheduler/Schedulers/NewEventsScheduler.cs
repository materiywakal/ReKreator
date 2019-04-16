using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using ReKreator.BL;
using ReKreator.Scheduler.Emailing;

namespace ReKreator.Scheduler.Schedulers
{
    public class NewEventsScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<NewEventsNotificationSender>().Build();
            job.JobDataMap["logger"] = Program.ServiceProvider.GetRequiredService<ILogger<NewEventsNotificationSender>>();
            job.JobDataMap["notificationHandler"] = Program.ServiceProvider.GetRequiredService<NotificationHandler>();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("NewEventsTrigger", "Emailers")
                .WithCronSchedule("0/1 0 14 ? * * *")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}