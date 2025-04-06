
using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Articles;
using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Articles.GetArticlesByCurrentUser
{
    public class GetArticlesByCurrentUserQueryHandler :
        IQueryHandler<GetArticlesByCurrentUserQuery, List<ArticleResponse>>
    {

        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public GetArticlesByCurrentUserQueryHandler(IArticleRepository articleRepository,
            IUserRepository userRepository,
            IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserIdAsync();
            var articles = await _articleRepository.GetArticlesByUserIdAsync(userId);
            var response = articles.Adapt<List<ArticleResponse>>();
            return response.OrderByDescending(x => x.DatePublished).ToList();
        }
    }
}
