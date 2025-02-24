using MediatR;

namespace BlazingBlog.Application.Articles.GetArticleById
{
    public class GetArticleByIdQuery : IRequest<ArticleResponse?>
    {
        public int Id { get; set; }
    }
}
