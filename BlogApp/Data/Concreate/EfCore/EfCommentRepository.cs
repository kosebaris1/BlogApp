using BlogApp.Data.AbstractBase;
using BlogApp.Entity;

namespace BlogApp.Data.Concreate.EfCore
{
    public class EfCommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public EfCommentRepository(BlogContext context)
        {
            _context = context;   
        }
        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment)
        {
           _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
