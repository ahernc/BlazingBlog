using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;

namespace BlazingBlog.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponse>
    {

        private readonly IArticleRepository _articleRepository;

        public CreateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = request.Adapt<Article>();
            var article = await _articleRepository.CreateArticleAsync(newArticle);
            return article.Adapt<ArticleResponse>();
        }
    }
}
