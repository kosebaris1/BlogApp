using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concreate.EfCore
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text="Web programlama", Url="web-programlama"},
                        new Tag { Text = "Backend", Url="backend" },
                        new Tag { Text = "Frontend", Url="frontend" },
                        new Tag { Text = "Fullstack",Url="fullstack" },
                        new Tag { Text = "Php",Url="php" }
                    );
                    context.SaveChanges();
                }
                 
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName="BarisKose"},
                        new User { UserName = "BurakKose" }
                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post {
                            Title = "Asp.net Core",
                            Content= "Asp.net Core dersleri",
                            Url="aspnet-core",
                            IsActive=true,
                            Image="post1.jpg",
                            publishedOn= DateTime.Now.AddDays(-10),
                            Tags= context.Tags.Take(3).ToList(),
                            UserId=1
                        },
                        new Post
                        {
                            Title = "Php Core",
                            Content = "Php Core dersleri",
                            Url="php",
                            IsActive = true,
                            Image="post2.jpg",
                            publishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "Django",
                            Content = "Django dersleri",
                            Url="django",
                            IsActive = true,
                            Image="post3.jpg",
                            publishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        },
                        new Post
                        {
                            Title = "React Dersleri",
                            Content = "React dersleri",
                            Url = "react-dersleri",
                            IsActive = true,
                            Image = "post1.jpg",
                            publishedOn = DateTime.Now.AddDays(-28),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "Angular",
                            Content = "Angular dersleri",
                            Url = "angular",
                            IsActive = true,
                            Image = "post2.jpg",
                            publishedOn = DateTime.Now.AddDays(-30),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "We Tasarım",
                            Content = "Web tasarım dersleri",
                            Url = "web-tasarım",
                            IsActive = true,
                            Image = "post3.jpg",
                            publishedOn = DateTime.Now.AddDays(-32),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }

                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
