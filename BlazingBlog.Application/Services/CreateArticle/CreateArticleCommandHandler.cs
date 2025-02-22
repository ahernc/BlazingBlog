using BlazingBlog.Domain.Articles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Services.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Article>
    {

        private readonly IArticleRepository _articleRepository;

        public CreateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = new Article
            {
                Title = request.Title,
                Content = request.Content,
                DateCreated = request.DatePublished,
                IsPublished = request.IsPublished
            };
            var article = await _articleRepository.CreateArticleAsync(newArticle);
            return article;
        }
    }
}
