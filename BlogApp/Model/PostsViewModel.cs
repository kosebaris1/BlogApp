﻿using BlogApp.Entity;

namespace BlogApp.Model
{
    public class PostsViewModel
    {
        public List<Post> Posts { get; set; } = new();

        public List<Tag> Tags { get; set; } = new();

    }
}
