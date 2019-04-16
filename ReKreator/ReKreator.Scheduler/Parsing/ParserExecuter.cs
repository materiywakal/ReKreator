using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using ReKreator.BL.Services;
using ReKreator.Parsing;
using ReKreator.Parsing.GenreDictionaries;

namespace ReKreator.Scheduler.Parsing
{
    class ParserExecuter : IJob
    {
        private const int _interval = 90;
        private ILogger<ParserExecuter> _logger;
        private ParsedDataHandler _dataHandler;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger = (ILogger<ParserExecuter>) context.JobDetail.JobDataMap["logger"];
            _dataHandler = (ParsedDataHandler) context.JobDetail.JobDataMap["dataHandler"];
            await ParseMoviesAsync();
            await ParseConcertsAsync();
            await ParsePerformancesAsync();
        }

        private async Task ParseMoviesAsync()
        {
            try
            {
                var movieParser = new MovieParser(_interval);
                var moviesResult = await movieParser.ParseAsync();
                await _dataHandler.SaveAsync(moviesResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        private async Task ParsePerformancesAsync()
        {
            try
            {
                 var performanceParser =
                    new GenericParser<PerformanceGenres>(_interval, "https://afisha.tut.by/day/theatre/");
                var performacesResult = await performanceParser.ParseAsync();
                await _dataHandler.SaveAsync(performacesResult);
            }
            catch (Exception e)
            {                
                _logger.LogError(e, e.Message);
            }
        }

        private async Task ParseConcertsAsync()
        {
            try
            {
                var concertParser = new GenericParser<ConcertGenres>(_interval, "https://afisha.tut.by/day/concert/");
                var concertsResult = await concertParser.ParseAsync();
                await _dataHandler.SaveAsync(concertsResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}