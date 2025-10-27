using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Eticaret.Service.Concrete;
using Eticaret.WebUI.ExtensionMethods;
using Eticaret.WebUI.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{

    public class CartController : Controller
    {
        //private readonly DatabaseContext _context;
        private readonly IService<Product> _productService;
        private readonly IService<Core.Entities.Address> _addressService;
        private readonly IService<Order> _orderService;
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IConfiguration _configuration;


        public CartController(IService<Product> productService, IService<Core.Entities.Address> addressService, DatabaseContext context, IService<Order> orderService, IService<AppUser> serviceAppUser, IConfiguration configuration)
        {
            _productService = productService;
            _addressService = addressService;
            //_context = context;
            _orderService = orderService;
            _serviceAppUser = serviceAppUser;
            _configuration = configuration;
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
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
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
        public async Task<IActionResult> Checkout(string CardNameSurname, string CardNumber,string CardMonth,string CardYear,string CVV,string IsDeliveryAddress, string IsBillingAddress)
        {
            var cart = GetCart();
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
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
                BillingAddress =$"{faturaAdresi.OpenAddress} {faturaAdresi.District} {faturaAdresi.City}" , //IsBillingAddress
                DeliveyAddress = $"{faturaAdresi.OpenAddress} {faturaAdresi.District} {faturaAdresi.City}", //IsDeliveryAddress,
                CustomerId = appUser.UserGuid.ToString(),
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice(),
                OrderNumber = Guid.NewGuid().ToString(),
                OrderState=0,
                OrderLines = []
            };
        
            #region OdemeIslemi
            Options options = new Options();
            options.ApiKey = _configuration["IyzicOptions:ApiKey"];
            options.SecretKey = _configuration["IyzicOptions:SecretKey"];
            options.BaseUrl = _configuration["IyzicOptions:BaseUrl"]; //"https://sandbox-api.iyzipay.com"

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = HttpContext.Session.Id;
            request.Price = siparis.TotalPrice.ToString().Replace(",", ".");
            request.PaidPrice = siparis.TotalPrice.ToString().Replace(",", ".");
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "MUSTAFA"+ HttpContext.Session.Id+"ERKEKLI";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = CardNameSurname; //"John Doe";
            paymentCard.CardNumber = CardNumber; //"5528790000000008";
            paymentCard.ExpireMonth = CardMonth;// "12";
            paymentCard.ExpireYear = CardYear; //"2030";
            paymentCard.Cvc = CVV; //"123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "ERKEKLI" + appUser.Id; ;
            buyer.Name = appUser.Name; //"John";
            buyer.Surname = appUser.Surname; //"Doe";
            buyer.GsmNumber = appUser.Phone; //"+905350000000";
            buyer.Email = appUser.Email;
            buyer.IdentityNumber = "99999999";
            buyer.LastLoginDate = DateTime.Now.ToString("yyy-mm-dd hh:mm:ss"); //"2015-10-05 12:43:35";
            buyer.RegistrationDate = appUser.CreateDate.ToString("yyy-mm-dd hh:mm:ss"); //"2013-04-21 15:12:09";
            buyer.RegistrationAddress = siparis.DeliveyAddress; //"Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = HttpContext.Connection.RemoteIpAddress?.ToString(); //"85.34.78.112";
            buyer.City = teslimatAdresi.City; //"Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            var shippingAddress = new Iyzipay.Model.Address();
            shippingAddress.ContactName = appUser.Name + ""+ appUser.Surname;
            shippingAddress.City = teslimatAdresi.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = teslimatAdresi.OpenAddress;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            var billingAddress = new Iyzipay.Model.Address();
            billingAddress.ContactName = appUser.Name + "" + appUser.Surname;
            billingAddress.City = faturaAdresi.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = teslimatAdresi.OpenAddress;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            //BasketItem firstBasketItem = new BasketItem();
            //firstBasketItem.Id = "BI101";
            //firstBasketItem.Name = "Binocular";
            //firstBasketItem.Category1 = "Collectibles";
            //firstBasketItem.Category2 = "Accessories";
            //firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //firstBasketItem.Price = "0.3";
            //basketItems.Add(firstBasketItem);

            foreach (var item in cart.CartLines)
            {
                siparis.OrderLines.Add(new OrderLine
                {
                    ProductId = item.Product.Id,
                    OrderId = siparis.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                });
                basketItems.Add(new BasketItem
                {
                    Id=item.Product.Id.ToString(),
                    Name = item.Product.Name,
                    Category1="Collectibles",
                    ItemType=BasketItemType.PHYSICAL.ToString(),
                    Price=(item.Product.Price*item.Quantity).ToString().Replace(",","."),

                });
            }

            if (siparis.TotalPrice<999)
            {
                basketItems.Add(new BasketItem
                {
                    Id = "Kargo",
                    Name = "Kargo Ücreti",
                    Category1 = "Collectibles",
                    ItemType = BasketItemType.VIRTUAL.ToString(),
                    Price = "99",

                });
                siparis.TotalPrice += 99;
                request.Price = siparis.TotalPrice.ToString().Replace(",", ".");
                request.PaidPrice = siparis.TotalPrice.ToString().Replace(",", ".");
            }


            request.BasketItems = basketItems;

            Payment payment =await Payment.Create(request, options);
         
            #endregion
            try
            {
                if (payment.Status == "success")
                {
                    //sipairş oluştur
                    await _orderService.AddAsync(siparis);
                    var response = await _orderService.SaveChangesAsync();
                    if (response > 0)
                    {
                        HttpContext.Session.Remove("Cart");
                        return RedirectToAction("Thanks");
                    }
                }
                else
                {
                    TempData["Message"] = $"<div class='alert alert-danger'> Ödeme İşlemi Başarısız!</div>({payment.ErrorMessage})";
                }
            }
            catch (Exception)
            {

                TempData["Message"] = "Hata Oluştu";
            }
            return View(model);
        }
        public async Task<IActionResult> Thanks()
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
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
