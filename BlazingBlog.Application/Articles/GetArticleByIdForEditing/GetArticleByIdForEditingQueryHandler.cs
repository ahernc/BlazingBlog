using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Articles;

namespace BlazingBlog.Application.Articles.GetArticleByIdForEditing
{
    public class GetArticleByIdForEditingQueryHandler : IQueryHandler<GetArticleByIdForEditingQuery, ArticleResponse?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public GetArticleByIdForEditingQueryHandler(IUserService userService, IArticleRepository articleRepository)
        {
            _userService = userService;
            _articleRepository = articleRepository;
        }

        public async Task<Result<ArticleResponse?>> Handle(GetArticleByIdForEditingQuery request, CancellationToken cancellationToken)
        {
            var canEdit = await _userService.CurrentUserCanEditArticleAsync(request.Id);
            if (!canEdit)
            {
                return Result.Fail<ArticleResponse?>("You are not allowed to edit this Article!");
            }

            var result = await _articleRepository.GetArticleByIdAsync(request.Id);
            var articleResponse = result.Adapt<ArticleResponse>();
            return articleResponse;
        }
    }
}
