using Blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class DetailController : Controller
    {
        private readonly DataContext _dataContext;

        public DetailController(DataContext datacontext)
        {
            _dataContext=datacontext;
        }
        public IActionResult Detail()
        {
            return View();
        }


        [HttpGet("detail")]
        public IActionResult Detail(int id)
        {
            var posts = _dataContext.Posts.Where(p => p.Id == id).FirstOrDefault();

            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

    }
}
