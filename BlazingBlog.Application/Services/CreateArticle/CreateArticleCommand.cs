using BlazingBlog.Domain.Articles;
using MediatR;

namespace BlazingBlog.Application.Services.CreateArticle
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;
    }
}
