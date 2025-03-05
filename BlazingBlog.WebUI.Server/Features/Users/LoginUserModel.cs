using System.ComponentModel.DataAnnotations;

namespace BlazingBlog.WebUI.Server.Features.Users
{
    public sealed class LoginUserModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = "";

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
    }
}
