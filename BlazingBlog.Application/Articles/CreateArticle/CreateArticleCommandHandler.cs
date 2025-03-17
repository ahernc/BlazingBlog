using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Articles;

namespace BlazingBlog.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, ArticleResponse>
    {

        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public CreateArticleCommandHandler(IArticleRepository articleRepository,
            IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleResponse>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newArticle = request.Adapt<Article>();

                newArticle.UserId = await _userService.GetCurrentUserIdAsync();
                if (await _userService.CurrentUserCanCreateArticleAsync() == false)
                {
                    return FailingResult();
                }

                var article = await _articleRepository.CreateArticleAsync(newArticle);

                return article.Adapt<ArticleResponse>();
            }
            catch (UserNotAuthorizedException)
            {
                return FailingResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Some strange error occurred while trying to create an article. {ex.Message}");
            }
        }

        private Result<ArticleResponse> FailingResult()
        {
            return Result.Fail<ArticleResponse>("You are not allowed to create articles. Sorry!");
        }
    }
}
