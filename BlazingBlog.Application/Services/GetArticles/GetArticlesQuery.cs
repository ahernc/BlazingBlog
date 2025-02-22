using BlazingBlog.Domain.Articles;
using MediatR;

namespace BlazingBlog.Application.Services.GetArticles
{
    public class GetArticlesQuery : IRequest<List<Article>>
    {

    }
}
