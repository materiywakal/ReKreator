using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum MovieGenresViewModel : ulong
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
    }
}
