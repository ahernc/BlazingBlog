using BlazingBlog.Domain.Articles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Services.GetArticles
{
    internal class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<Article>>
    {
        private readonly IArticleRepository _articleRepository;

        public GetArticlesQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<List<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _articleRepository.GetAllArticlesAsync();
            return articles;
        }
    }
}
