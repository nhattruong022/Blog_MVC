using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Blog.Controllers
{
	public class SearchController : Controller
	{
		private readonly DataContext _datacontext;

		public SearchController(DataContext datacontext)
		{
			_datacontext= datacontext;
        }

        [HttpGet("search")]
		public IActionResult Search(string keyword)
		{
          var posts=string.IsNullOrEmpty(keyword)
				?_datacontext.Posts.ToList() //neu khong tim thay thi hien full posts
				:_datacontext.Posts.Where(p=>p.Title.Contains(keyword)).ToList();
            return View("Search",posts);
		}

	}
}
