using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using ReKreator.BL.Services;
using ReKreator.Scheduler.Parsing;

namespace ReKreator.Scheduler.Schedulers
{
    public class ParserScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ParserExecuter>().Build();
            job.JobDataMap["logger"] = Program.ServiceProvider.GetRequiredService<ILogger<ParserExecuter>>();
            job.JobDataMap["dataHandler"] = Program.ServiceProvider.GetRequiredService<ParsedDataHandler>();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SomeParser", "Parsers")
                .WithCronSchedule("0/1 0 0,12 ? * * *")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
