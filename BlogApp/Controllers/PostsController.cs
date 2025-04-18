﻿using BlogApp.Data.AbstractBase;
using BlogApp.Data.Concreate.EfCore;
using BlogApp.Entity;
using BlogApp.Model;
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
            var posts = _postRepository.Posts;
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

    }
}
