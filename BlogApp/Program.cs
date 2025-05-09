using BlogApp.Data.AbstractBase;
using BlogApp.Data.Concreate.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<BlogContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPostRepository, EfPostRepository>();
            builder.Services.AddScoped<ITagRepository, EfTagRepository>();
            builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
            builder.Services.AddScoped<IUsersRepository, EfUsersRepository>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(optinos =>
            {
                optinos.LoginPath = "/Users/Login";
            });

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            SeedData.TestVerileriniDoldur(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "post_details",
                pattern: "posts/details/{url}",
                defaults: new{controller="Posts",action="Details"}
            );

            app.MapControllerRoute(
                name: "posts_by_tag",
                pattern: "posts/tag/{tag}",
                defaults: new { controller = "Posts", action = "Index" }
            );

            app.MapControllerRoute(
               name: "user_profile",
               pattern: "profile/{username}",
               defaults: new { controller = "Users", action = "Profile" }
           );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Posts}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
