using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum TheatreGenresViewModel : ulong
    {
        None = 0,

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
