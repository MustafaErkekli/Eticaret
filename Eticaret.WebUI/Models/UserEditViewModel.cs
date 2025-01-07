using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Models
{
    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
