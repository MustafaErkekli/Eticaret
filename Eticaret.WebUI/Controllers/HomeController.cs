using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eticaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<Slider> _sliderService;
        private readonly IService<News> _newService;
        private readonly IService<Contact> _contactService;

        public HomeController(IService<Product> productService, IService<Slider> sliderService, IService<News> newService, IService<Contact> contactService)
        {
            _productService = productService;
            _sliderService = sliderService;
            _newService = newService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Products = await _productService.GetAllAsync(p => p.IsActive && p.IsHome),
                News = await _newService.GetAllAsync(),
            };
            return View(model);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactService.AddAsync(contact);
                    var response =await _contactService.SaveChangesAsync();
                    if (response > 0)

                    {
                        //await MailHelper.SendMailAsync(contact);
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Mesajýnýz Gönderilmiþtir.</strong>
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
  </div>";
                    
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata oluþtu");
                }
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
