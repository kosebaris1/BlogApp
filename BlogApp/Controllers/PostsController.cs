using BlogApp.Data.AbstractBase;
using BlogApp.Data.Concreate.EfCore;
using BlogApp.Entity;
using BlogApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
       

        public PostsController(IPostRepository postRepository,ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
           
        }

        public async Task<IActionResult> Index(string tag)  
        {
            var claims= User.Claims;
            var posts = _postRepository.Posts.Where(i=> i.IsActive);
            if (!string.IsNullOrEmpty(tag))
            {
                posts= posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }
            return View(
                new PostsViewModel
                {
                    Posts= await posts.ToListAsync(),
               
                });
        }

        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                             .Posts
                             .Include(x => x.User)
                             .Include(x => x.Tags)
                             .Include(x => x.Comments)
                             .ThenInclude(x => x.User)
                             .FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var image = User.FindFirstValue(ClaimTypes.UserData);
            var entity = new Comment
            {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId=int.Parse(userId ?? ""),
            };

            _commentRepository.CreateComment(entity);
            try
            {
                return Json(new
                {
                    userName,
                    entity.Text,
                    publishedOn = entity.PublishedOn.ToString(),
                    image
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Description=model.Description,
                        Url = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        publishedOn= DateTime.Now,
                        Image="post1.jpg",
                        IsActive=false
                    }
                    );
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");   
            var userName=User.FindFirstValue(ClaimTypes.Name);
            var role=User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
                posts= posts.Where(x => x.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { 
             return NotFound();
            }
            var post = _postRepository.Posts.FirstOrDefault(i => i.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(new PostCreateViewModel
            {
                PostId=post.PostId,
                Title=post.Title,
                Description=post.Description,
                Content=post.Content,
                Url=post.Url,
                IsActive=post.IsActive,
            });
           
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                    IsActive = model.IsActive,

                };
                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityToUpdate.IsActive = model.IsActive; 
                }
                _postRepository.EditPost(entityToUpdate);
                return RedirectToAction("List");
            }
           
                return View(model);
        }

    }
}
