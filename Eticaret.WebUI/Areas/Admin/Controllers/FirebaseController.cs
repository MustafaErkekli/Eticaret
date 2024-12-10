using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    public class FirebaseController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "",
            BasePath = "https://databaseName.firebaseio.com/"
        };

        IFirebaseClient client;

        private void Connection()
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
                Console.WriteLine("bağlantı sağlandı");
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}
