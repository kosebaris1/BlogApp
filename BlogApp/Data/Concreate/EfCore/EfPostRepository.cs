using BlogApp.Data.AbstractBase;
using BlogApp.Entity;

namespace BlogApp.Data.Concreate.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private readonly BlogContext _Context;
        public EfPostRepository(BlogContext context)
        {
            _Context = context;
        }
        public IQueryable<Post> Posts => _Context.Posts;

        public void CreatePost(Post post)
        {
           _Context.Posts.Add(post);
            _Context.SaveChanges();
        }
    }
}
