using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum ConcertGenresViewModel : ulong
    {
        None = 0,

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
    }
}
