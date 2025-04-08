using BlogApp.Data.AbstractBase;
using BlogApp.Data.Concreate.EfCore;
using BlogApp.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
