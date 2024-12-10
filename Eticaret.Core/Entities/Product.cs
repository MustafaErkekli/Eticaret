using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
	public class Product : IEntity
	{
		public int Id { get; set; }

        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklamalar")]
        public string? Description { get; set; }

        [Display(Name = "Resim")]
        public string? Image { get; set; }

        [Display(Name = "Ücret")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Kodu")]
        public string? ProductCode { get; set; }

        [Display(Name = "Stok")]
        public int Stock { get; set; }

        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }

        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }
		public int? CategoryId { get; set; }
        public Category Category { get; set; }
		public int? BrandId { get; set; }
		public Brand Brand { get; set; }

        [Display(Name = "Sıra Numarası")]
        public int OrderNo { get; set; }

        [Display(Name = "Kayıt Tarihi?"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }= DateTime.Now;
	}
}
