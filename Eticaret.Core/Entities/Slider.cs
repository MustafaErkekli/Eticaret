using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
	public class Slider :IEntity
	{
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        [Display(Name = "Açıklamalar")]
        public string? Description { get; set; }

        [Display(Name = "Resim")]
        public string? Image { get; set; }

        [Display(Name = "Link")]
        public string? Link { get; set; }
    }
}
