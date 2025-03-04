using System.ComponentModel.DataAnnotations;

namespace BlazingBlog.WebUI.Server.Features.Users
{
    public sealed class LoginUserModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "User name")]
        public string UserName { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
    }
}
