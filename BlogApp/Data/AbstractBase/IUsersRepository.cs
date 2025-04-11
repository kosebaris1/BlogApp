using BlogApp.Entity;

namespace BlogApp.Data.AbstractBase
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }

    }
}
