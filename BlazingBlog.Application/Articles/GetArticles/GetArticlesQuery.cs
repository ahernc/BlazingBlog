using BlazingBlog.Domain.Articles;
using MediatR;

namespace BlazingBlog.Application.Articles.GetArticles
{
    public class GetArticlesQuery : IRequest<List<ArticleResponse>>
    {

    }
}
