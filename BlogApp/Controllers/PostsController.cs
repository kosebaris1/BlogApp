using BlogApp.Data.AbstractBase;
using BlogApp.Data.Concreate.EfCore;
using BlogApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
       

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
           
        }

        public IActionResult Index()
        {
            return View(
                new PostsViewModel
                {
                    Posts= _postRepository.Posts.ToList(),
               
                });
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View(await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == id));
        }
    }
}
