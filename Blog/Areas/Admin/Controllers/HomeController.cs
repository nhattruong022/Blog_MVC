using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes ="AdminCookie")]
    public class HomeController : Controller
    {

        [Authorize(Roles ="Admin")]
        [HttpGet("Admin/Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
