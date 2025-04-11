using BlogApp.Data.AbstractBase;
using BlogApp.Entity;

namespace BlogApp.Data.Concreate.EfCore
{
    public class EfUsersRepository : IUsersRepository
    {
        private readonly BlogContext _context;

        public EfUsersRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;
    }
}
