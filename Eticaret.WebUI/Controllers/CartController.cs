using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Eticaret.Service.Concrete;
using Eticaret.WebUI.ExtensionMethods;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{

    public class CartController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IService<Product> _productService;
        private readonly IService<Address> _addressService;
        private readonly IService<Order> _orderService;


        public CartController(IService<Product> productService, IService<Address> addressService, DatabaseContext context, IService<Order> orderService)
        {
            _productService = productService;
            _addressService = addressService;
            _context = context;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var cart = GetCart();
            var model = new CartViewModel()
            {
                CartLines=cart.CartLines,
                TotalPrice=cart.TotalPrice()
                
            };
            return View(model);
        }
        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart") ?? new CartService();
        }
        public IActionResult Add(int ProductId, int quantity = 1)
        {
            var product = _productService.Find(ProductId);
            if (product != null)
            {
                var cart=GetCart();
                cart.AddProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int ProductId, int quantity = 1)
        {
            var product = _productService.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.UpdateProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public  async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x=>x.UserGuid.ToString()==HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser==null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses =await _addressService.GetAllAsync(x=>x.AppUserId==appUser.Id&&x.IsActive);
            var model = new CheckoutViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses= addresses
            };
            return View(model);
        }

        [Authorize,HttpPost]
        public async Task<IActionResult> Checkout(string CardNumber,string CardMonth,string CardYear,string CVV,string IsDeliveryAddress, string IsBillingAddress)
        {
            var cart = GetCart();
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses = await _addressService.GetAllAsync(x => x.AppUserId == appUser.Id && x.IsActive);
            var model = new CheckoutViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            if (string.IsNullOrWhiteSpace(CardNumber)  || string.IsNullOrWhiteSpace(CardMonth) || string.IsNullOrWhiteSpace(CardYear) || string.IsNullOrWhiteSpace(CVV) || string.IsNullOrWhiteSpace(IsDeliveryAddress) || string.IsNullOrWhiteSpace(IsBillingAddress))
            {
                return View(model);
            }
            var teslimatAdresi=addresses.FirstOrDefault(x=>x.AddressGuid.ToString()== IsDeliveryAddress);
            var faturaAdresi=addresses.FirstOrDefault(x=>x.AddressGuid.ToString()==IsBillingAddress);

            //Ödeme Çekme işlemi burda yapılıyor
            var siparis = new Order
            {
                AppUserId = appUser.Id.GetHashCode(),
                BillingAddress = IsBillingAddress,
                CustomerId = appUser.UserGuid.ToString(),
                DeliveyAddress = IsDeliveryAddress,
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice(),
                OrderNumber = Guid.NewGuid().ToString(),
                OrderLines = []
            };
            foreach (var item in cart.CartLines)
            {
                siparis.OrderLines.Add(new OrderLine
                {
                    ProductId=item.Product.Id,
                    OrderId=siparis.Id,
                    Quantity=item.Quantity, 
                    UnitPrice=item.Product.Price,
                });
            }
            try
            {
                await _orderService.AddAsync(siparis);
                var response=await _orderService.SaveChangesAsync();
                if (response>0)
                {
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Thanks");
                }
            }
            catch (Exception)
            {

                TempData["Message"] = "Hata Oluştu";
            }
            return View(model);
        }
        public async Task<IActionResult> ThanksAsync()
        {
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            return View(appUser);
        }
        public IActionResult Remove(int ProductId)
        {
            var product = _productService.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

    }
}
