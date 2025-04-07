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
                        new Tag { Text="Web programlama"},
                        new Tag { Text = "Backend" },
                        new Tag { Text = "Frontend" },
                        new Tag { Text = "Fullstack" },
                        new Tag { Text = "Php" }
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
                            IsActive=true,
                            publishedOn= DateTime.Now.AddDays(-10),
                            Tags= context.Tags.Take(3).ToList(),
                            UserId=1
                        },
                        new Post
                        {
                            Title = "Php Core",
                            Content = "Php Core dersleri",
                            IsActive = true,
                            publishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "Django",
                            Content = "Django dersleri",
                            IsActive = true,
                            publishedOn = DateTime.Now.AddDays(-5),
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
