using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Model
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }

        [Required]
        [Display(Name="Başlık")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }

        [Required]
        [Display(Name = "url")]
        public string? Url { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<Tag> Tags { get; set; } = new();


    }
}
