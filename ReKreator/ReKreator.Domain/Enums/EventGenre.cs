using System;
using System.ComponentModel;

namespace ReKreator.Domain.Enums
{
    [Flags]
    public enum EventGenre : ulong
    {
        [Description("")]
        None = 0,

        [Description("Animation")] MovieAnimation = 0x00000001,
        [Description("Biography")] MovieBiography = 0x00000002,
        [Description("Action")] MovieAction = 0x00000004,
        [Description("Military")] MovieMilitary = 0x00000008,
        [Description("Family")] MovieFamily = 0x00000010,
        [Description("Detective")] MovieDetective = 0x00000020,
        [Description("Documentary")] MovieDocumentary = 0x00000040,
        [Description("Drama")] MovieDrama = 0x00000080,
        [Description("Historical")] MovieHistorical = 0x00000100,
        [Description("Comedy")] MovieComedy = 0x00000200,
        [Description("Criminal")] MovieCriminal = 0x00000400,
        [Description("Melodrama")] MovieMelodrama = 0x00000800,
        [Description("Musical")] MovieMusical = 0x00001000,
        [Description("Adventure")] MovieAdventure = 0x00002000,
        [Description("Thriller")] MovieThriller = 0x00004000,
        [Description("Horror")] MovieHorror = 0x00008000,
        [Description("Fantastic")] MovieFantastic = 0x00010000,
        [Description("Fantasy")] MovieFantasy = 0x00020000,
        [Description("Sport")] MovieSport = 0x00040000,

        [Description("Alternative")] ConcertAlternative = 0x001000000,
        [Description("Jazz and blues")] ConcertJazzAndBlues = 0x002000000,
        [Description("Classic")] ConcertClassic = 0x004000000,
        [Description("Pop")] ConcertPop = 0x008000000,
        [Description("Festival")] ConcertFestival = 0x010000000,
        [Description("Hip-hop and rap")] ConcertHipHopRap = 0x020000000,
        [Description("Chanson")] ConcertChanson = 0x040000000,
        [Description("Humor")] ConcertHumor = 0x080000000,
        [Description("Rock")] ConcertRock = 0x100000000,
        [Description("Metal")] ConcertMetal = 0x200000000,

        [Description("Puppet")] TheatrePuppet = 0x10000000000,
        [Description("Melodrama")] TheatreMelodrama = 0x20000000000,
        [Description("Music")] TheatreMusic = 0x40000000000,
        [Description("Vaudeville")] TheatreVaudeville = 0x80000000000,
        [Description("Musical")] TheatreMusical = 0x100000000000,
        [Description("Opera")] TheatreOpera = 0x200000000000,
        [Description("Ballet")] TheatreBallet = 0x400000000000,
        [Description("Drama")] TheatreDrama = 0x800000000000,
        [Description("Comedy")] TheatreComedy = 0x1000000000000,
        [Description("Child")] TheatreChild = 0x2000000000000,
        [Description("Solo perfomance")] TheatreSoloPerformance = 0x4000000000000,
        [Description("Operetta")] TheatreOperetta = 0x8000000000000,
        [Description("Tragedy")] TheatreTragedy = 0x10000000000000,
        [Description("Tragic comedy")] TheatreTragicomedy = 0x20000000000000
    }
}