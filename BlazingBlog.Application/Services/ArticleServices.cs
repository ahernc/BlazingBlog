using BlazingBlog.Domain.Articles;
namespace BlazingBlog.Application.Services
{
    public class ArticleServices : IArticleService
    { 
        public List<Article> GetAllArticles()
        {
            return new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "Title 1",
                    Content = "Some brilliant blog"             
                },
                new Article
                {
                    Id = 2,
                    Title = "2nd Article",
                    Content = "Another article published"
                }
            };
        }
    }
}
