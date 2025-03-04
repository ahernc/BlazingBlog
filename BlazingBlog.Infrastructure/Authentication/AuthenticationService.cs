using BlazingBlog.Application.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BlazingBlog.Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            // come back to set the isPersistent from the checkbox... 
            // 4th param is lockout -- we can come back to this
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true, // assume true for now -- confirmation is not required for now
            };
            var result = await _userManager.CreateAsync(user, password);
            var response = new RegisterUserResponse
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
            return response;
        }

    }

}
