using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;

namespace BlazingBlog.Application.Articles.GetArticleById
{
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleResponse?>
    {
        private readonly IArticleRepository _articleRepository;

        public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ArticleResponse?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.GetArticleByIdAsync(request.Id);
            if (result is null)
            {
                return null;
            }
            else
            {
                return result.Adapt<ArticleResponse>();
            }
        }
    }
}
