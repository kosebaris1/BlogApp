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
                        new Tag { Text="Web programlama", Url="web-programlama", Colors=TagColors.warning},
                        new Tag { Text = "Backend", Url="backend", Colors=TagColors.success},
                        new Tag { Text = "Frontend", Url="frontend", Colors = TagColors.secondary },
                        new Tag { Text = "Fullstack",Url="fullstack", Colors = TagColors.success },
                        new Tag { Text = "Php",Url="php", Colors = TagColors.primary }
                    );
                    context.SaveChanges();
                }
                 
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName="BarisKose",Image="p1.jpg"},
                        new User { UserName = "BurakKose",Image="p2.jpg" }
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
                            Tags= context.Tags.Take(2).ToList(),
                            UserId=1,
                            Comments = new List<Comment>
                            {
                                new Comment {Text= "İyi bir kurs", PublishedOn=new DateTime(),UserId=1},
                                new Comment {Text= "Çok faydalandığım bir kurs", PublishedOn=new DateTime(),UserId=2},
                            }
                        },
                        new Post
                        {
                            Title = "Php Core",
                            Content = "Php Core dersleri",
                            Url="php",
                            IsActive = true,
                            Image="post2.jpg",
                            publishedOn = DateTime.Now.AddDays(-20),
                            Tags = context
                                  .Tags
                                  .Where(x => x.Text == "php")
                                  .ToList(),
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
                            Tags = context.Tags.Skip(3).Take(2).ToList(),
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
                            Tags = context.Tags.Take(4).ToList(),
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
                            Title = "Web Tasarım",
                            Content = "Web tasarım dersleri",
                            Url = "web-tasarım",
                            IsActive = true,
                            Image = "post3.jpg",
                            publishedOn = DateTime.Now.AddDays(-32),
                            Tags = context.Tags.Take(1).ToList(),
                            UserId = 2
                        }

                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
