using BlazingBlog.Domain.Articles;

namespace BlazingBlog.Application.Services
{
    public interface IArticleService
    {
        List<Article> GetAllArticles();
    }
}
