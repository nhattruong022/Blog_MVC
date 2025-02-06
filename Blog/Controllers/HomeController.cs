using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
        public HomeController(ILogger<HomeController> logger,DataContext datacontext)
        {
            _logger = logger;
            _dataContext=datacontext;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var posts = _dataContext.Posts.ToList();

            return View(posts);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
