using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum MovieGenresViewModel : ulong
    {
        [Display(Name = "-")] None = 0,

        [Display(Name = "Анимация")] MovieAnimation = 0x00000001,
        [Display(Name = "Биография")] MovieBiography = 0x00000002,
        [Display(Name = "Экшн")] MovieAction = 0x00000004,
        [Display(Name = "Военное")] MovieMilitary = 0x00000008,
        [Display(Name = "Семейное")] MovieFamily = 0x00000010,
        [Display(Name = "Детектив")] MovieDetective = 0x00000020,
        [Display(Name = "Документальное")] MovieDocumentary = 0x00000040,
        [Display(Name = "Драма")] MovieDrama = 0x00000080,
        [Display(Name = "Историческое")] MovieHistorical = 0x00000100,
        [Display(Name = "Комедия")] MovieComedy = 0x00000200,
        [Display(Name = "Криминальное")] MovieCriminal = 0x00000400,
        [Display(Name = "Мелодрама")] MovieMelodrama = 0x00000800,
        [Display(Name = "Музыкальное")] MovieMusical = 0x00001000,
        [Display(Name = "Приключение")] MovieAdventure = 0x00002000,
        [Display(Name = "Триллер")] MovieThriller = 0x00004000,
        [Display(Name = "Ужасы")] MovieHorror = 0x00008000,
        [Display(Name = "Фантастика")] MovieFantastic = 0x00010000,
        [Display(Name = "Фэнтези")] MovieFantasy = 0x00020000,
        [Display(Name = "Спорт")] MovieSport = 0x00040000,
    }
}