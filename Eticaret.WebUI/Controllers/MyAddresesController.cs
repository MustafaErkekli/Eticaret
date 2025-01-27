using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{
    [Authorize]
    public class MyAddresesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IService<Address> _addressService;

        public MyAddresesController(DatabaseContext context, IService<Address> addressService)
        {
            _context = context;
            _addressService = addressService;
        }
        public async Task<IActionResult> Index()
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası Bulunamadı!Oturumunuzu Kapatıp Lütfen Tekrar Giriş Yapınız!");
            }
            var model = await _addressService.GetAllAsync(u => u.AppUserId == appUser.Id);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);            
                    if (appUser != null)
                    {
                        address.AppUserId = appUser.Id;
                        address.IsActive = true;
                        _addressService.Add(address);
                        await _addressService.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            
            }
            ModelState.AddModelError("", "Kayıt Başarısız");
            return View(address);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası Bulunamadı!Oturumunuzu Kapatıp Lütfen Tekrar Giriş Yapınız!");
            }
            var model = await _addressService.GetAsync(u => u.AddressGuid.ToString() == id&&u.AppUserId==appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,Address address)
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası Bulunamadı!Oturumunuzu Kapatıp Lütfen Tekrar Giriş Yapınız!");
            }
            var model = await _addressService.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            model.Title = address.Title;
            model.District = address.District;
            model.City = address.City;
            model.OpenAddress = address.OpenAddress;
            model.IsDeliveryAddress = address.IsDeliveryAddress;
            model.IsBillingAddress = address.IsBillingAddress;
            model.IsActive = address.IsActive;
            var otherAddresses=await _addressService.GetAllAsync(x=>x.AppUserId==appUser.Id && x.Id!=model.Id);
            foreach (var otherAddress in otherAddresses)
            {
                otherAddress.IsDeliveryAddress=false;
                otherAddress.IsBillingAddress=false;
                _addressService.Update(otherAddress);
            }
            try
            {
                _addressService.Update(model);
                await _context.SaveChangesAsync();  
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası Bulunamadı!Oturumunuzu Kapatıp Lütfen Tekrar Giriş Yapınız!");
            }
            var model = await _addressService.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id,Address address)
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası Bulunamadı!Oturumunuzu Kapatıp Lütfen Tekrar Giriş Yapınız!");
            }
            var model = await _addressService.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            try
            {
                _addressService.Delete(model);
                await _addressService.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(model);
        }
    }
}
