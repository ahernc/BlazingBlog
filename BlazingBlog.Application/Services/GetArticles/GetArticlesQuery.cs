using BlazingBlog.Domain.Articles;
using MediatR;

namespace BlazingBlog.Application.Services.GetArticles
{
    internal class GetArticlesQuery : IRequest<List<Article>>
    {

    }
}
