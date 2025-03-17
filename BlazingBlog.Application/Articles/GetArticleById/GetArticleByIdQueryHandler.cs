using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Articles;
using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Articles.GetArticleById
{
    public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, ArticleResponse?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public GetArticleByIdQueryHandler(IArticleRepository articleRepository, IUserRepository userRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleResponse?>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleByIdAsync(request.Id);
            var articleResponse = new ArticleResponse();

            if (article is null)
            {
                return Result.Fail<ArticleResponse?>("The article does not exist.");
            }
            else
            {
                articleResponse = article.Adapt<ArticleResponse>();
                if (article.UserId is not null)
                {
                    var author = await _userRepository.GetUserByIdAsync(article.UserId);
                    articleResponse.UserName = author?.UserName ?? "Unknown";
                    articleResponse.UserId = article.UserId;
                    articleResponse.CanEdit = await _userService.CurrentUserCanEditArticleAsync(article.Id);
                }
                else
                {
                    articleResponse.UserName = "Unknown";
                }
                return articleResponse;
            }
        }
    }
}
