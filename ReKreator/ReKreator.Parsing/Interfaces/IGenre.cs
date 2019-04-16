using System.Collections.Generic;
using ReKreator.Domain.Enums;

namespace ReKreator.Parsing.Interfaces
{
    public interface IGenre
    {
        EventType EventType { get; }
        Dictionary<string, EventGenre> Container { get; }
    }
}
