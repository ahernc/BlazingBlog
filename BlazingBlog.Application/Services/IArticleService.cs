using BlazingBlog.Domain.Articles;

namespace BlazingBlog.Application.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
    }
}
