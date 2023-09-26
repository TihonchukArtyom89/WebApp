using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Common()
        {
            return View();
        }
    }
}
