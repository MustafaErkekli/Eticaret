using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }

        [Display(Name ="Adres Başlığı"), StringLength(250), Required(ErrorMessage = "{0} Alanı zorunludur!")]
        public string Title { get; set; }

        [Display(Name = "İl"), StringLength(250), Required(ErrorMessage = "{0} Alanı zorunludur!")]
        public string City { get; set; }

        [Display(Name = "İlçe"), StringLength(250), Required(ErrorMessage = "{0} Alanı zorunludur!")]
        public string District { get; set; }

                                       //Çoklu satır ekleme
        [Display(Name = "Açık Adres"),DataType(DataType.MultilineText), Required(ErrorMessage = "{0} Alanı zorunludur!")]
        public string OpenAddress { get; set; }

        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }

        [Display(Name = "Fatura Adresi")]
        public bool IsBillingAddress { get; set; }

        [Display(Name ="Teslimat Adresi")]
        public bool IsDeliveryAddress { get; set; }

        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        public Guid? AddressGuid { get; set; } = Guid.NewGuid();

        [Display(Name = "Kullanıcı Mail")]
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
