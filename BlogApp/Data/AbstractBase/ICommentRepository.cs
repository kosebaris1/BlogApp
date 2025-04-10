using BlogApp.Entity;

namespace BlogApp.Data.AbstractBase
{
    public interface ICommentRepository
    {
         IQueryable<Comment> Comments { get; }

        void CreateComment(Comment comment);




    }
}
