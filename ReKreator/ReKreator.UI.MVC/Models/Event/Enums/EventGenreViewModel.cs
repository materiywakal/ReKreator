using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum EventGenreViewModel : ulong
    {
        None = 0,

        [Display(Name = "Animation")] MovieAnimation = 0x00000001,
        [Display(Name = "Biography")] MovieBiography = 0x00000002,
        [Display(Name = "Action")] MovieAction = 0x00000004,
        [Display(Name = "Military")] MovieMilitary = 0x00000008,
        [Display(Name = "Family")] MovieFamily = 0x00000010,
        [Display(Name = "Detective")] MovieDetective = 0x00000020,
        [Display(Name = "Documentary")] MovieDocumentary = 0x00000040,
        [Display(Name = "Drama")] MovieDrama = 0x00000080,
        [Display(Name = "Historical")] MovieHistorical = 0x00000100,
        [Display(Name = "Comedy")] MovieComedy = 0x00000200,
        [Display(Name = "Criminal")] MovieCriminal = 0x00000400,
        [Display(Name = "Melodrama")] MovieMelodrama = 0x00000800,
        [Display(Name = "Musical")] MovieMusical = 0x00001000,
        [Display(Name = "Adventure")] MovieAdventure = 0x00002000,
        [Display(Name = "Thriller")] MovieThriller = 0x00004000,
        [Display(Name = "Horror")] MovieHorror = 0x00008000,
        [Display(Name = "Fantastic")] MovieFantastic = 0x00010000,
        [Display(Name = "Fantasy")] MovieFantasy = 0x00020000,
        [Display(Name = "Sport")] MovieSport = 0x00040000,

        [Display(Name = "Alternative")] ConcertAlternative = 0x001000000,
        [Display(Name = "Jazz and blues")] ConcertJazzAndBlues = 0x002000000,
        [Display(Name = "Classic")] ConcertClassic = 0x004000000,
        [Display(Name = "Pop")] ConcertPop = 0x008000000,
        [Display(Name = "Festival")] ConcertFestival = 0x010000000,
        [Display(Name = "Hip-hop and rap")] ConcertHipHopRap = 0x020000000,
        [Display(Name = "Chanson")] ConcertChanson = 0x040000000,
        [Display(Name = "Humor")] ConcertHumor = 0x080000000,
        [Display(Name = "Rock")] ConcertRock = 0x100000000,
        [Display(Name = "Metal")] ConcertMetal = 0x200000000,

        [Display(Name = "Puppet")] TheatrePuppet = 0x10000000000,
        [Display(Name = "Melodrama")] TheatreMelodrama = 0x20000000000,
        [Display(Name = "Music")] TheatreMusic = 0x40000000000,
        [Display(Name = "Vaudeville")] TheatreVaudeville = 0x80000000000,
        [Display(Name = "Musical")] TheatreMusical = 0x100000000000,
        [Display(Name = "Opera")] TheatreOpera = 0x200000000000,
        [Display(Name = "Ballet")] TheatreBallet = 0x400000000000,
        [Display(Name = "Drama")] TheatreDrama = 0x800000000000,
        [Display(Name = "Comedy")] TheatreComedy = 0x1000000000000,
        [Display(Name = "Child")] TheatreChild = 0x2000000000000,
        [Display(Name = "Solo perfomance")] TheatreSoloPerformance = 0x4000000000000,
        [Display(Name = "Operetta")] TheatreOperetta = 0x8000000000000,
        [Display(Name = "Tragedy")] TheatreTragedy = 0x10000000000000,
        [Display(Name = "Tragic comedy")] TheatreTragicomedy = 0x20000000000000
    }
}
