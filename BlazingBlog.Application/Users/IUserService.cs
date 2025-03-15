namespace BlazingBlog.Application.Users
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<bool> IsCurrentUserInRoleAsync(string role);
        Task<bool> CurrentUserCanCreateArticleAsync();
        Task<bool> CurrentUserCanEditArticleAsync(int articleId);
    }
}
