﻿using BlogApp.Entity;

namespace BlogApp.Data.AbstractBase
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        void CreatePost(Post post);


    }
}
