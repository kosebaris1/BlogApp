using System.ComponentModel.DataAnnotations;

namespace BlogApp.Model
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="Max 10 karakter giriniz.",MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }
}
