using System.Threading.Tasks;
using ReKreator.Domain;

namespace ReKreator.Parsing.Interfaces
{
    public interface IParser
    {
        /// <summary>
        /// Parses events from the web page "afisha.tut.by".
        /// </summary>
        /// <returns>Task of model which contains Events, EventsPlaces and EventsHoldings collections</returns>
        Task<ParsingModel> ParseAsync();
    }
}
