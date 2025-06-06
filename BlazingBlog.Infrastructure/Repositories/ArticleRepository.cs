﻿using BlazingBlog.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace BlazingBlog.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {

        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var articleToDelete = await _context.Articles.FindAsync(id);
            if (articleToDelete is null)
            {
                return false;
            }
            _context.Articles.Remove(articleToDelete);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            // To do: rename and allow filter by Published only... that's the whole point of the published flag
            return await _context.Articles.Where(x => x.IsPublished).ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            return article;
        }

        public async Task<List<Article>> GetArticlesByUserIdAsync(string userId)
        {
            return await _context.Articles
                .Where(article => article.UserId == userId)
                .ToListAsync();
        }

        public async Task<Article?> UpdateArticleAsync(Article article)
        {
            var articleToUpdate = await GetArticleByIdAsync(article.Id);
            if (articleToUpdate is null)
            {
                return null;
            }

            articleToUpdate.Title = article.Title;
            articleToUpdate.Content = article.Content;
            articleToUpdate.DatePublished = article.DatePublished;
            articleToUpdate.DateUpdated = DateTime.Now;
            articleToUpdate.IsPublished = article.IsPublished;
            article.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
            return articleToUpdate;
        }

    }
}
