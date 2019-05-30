using System.Configuration;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ReKreator.BL;
using ReKreator.BL.Services;
using ReKreator.DAL;
using ReKreator.DAL.Interfaces;
using ReKreator.Emailing;
using ReKreator.Scheduler.Emailing;
using ReKreator.Scheduler.Parsing;
using ReKreator.Scheduler.Schedulers;

namespace ReKreator.Scheduler
{
    public class Program
    {
        public static ServiceProvider ServiceProvider { get; } = BuildServiceProvider();

        public static void Main(string[] args)
        {
            try
            {
                NewEventsScheduler.Start();
                FavoritesScheduler.Start();
                ParserScheduler.Start();
                Thread.CurrentThread.Join();
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventContext>();
            var options = optionsBuilder
                .UseSqlServer(
                    ConfigurationManager.ConnectionStrings["mw"].ToString())
                .Options;
            var eventContext = new EventContext(options);

            return new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .AddTransient<ParserExecuter>()
                .AddTransient<FavoritesNotificationSender>()
                .AddTransient<NewEventsNotificationSender>()
                .AddScoped<IUnitOfWork>(o => new EventUnitOfWork(eventContext))
                .AddSingleton<ISender>(o => new Sender(ConfigurationManager.AppSettings["apiKey"],
                    ConfigurationManager.AppSettings["email"]))
                .AddSingleton<ParsedDataHandler>()
                .AddSingleton<NotificationHandler>()
                .BuildServiceProvider();
        }
    }
}