using BlogApp.Entity;
namespace BlogApp.Data.AbstractBase
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags {  get; } 

        void CreateTag(Tag tag);
    }
}
