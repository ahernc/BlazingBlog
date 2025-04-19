using BlazingBlog.Application.Articles;
using BlazingBlog.Application.Articles.GetArticlesByCurrentUser;
using BlazingBlog.Application.Articles.TogglePublishArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazingBlog.WebUI.Server.Features.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ISender _sender;

        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleResponse>>> GetArticlesByCurrentUser()
        {
            // We could use the similar approach and use a service again... 
            // but it's fine here to just send it
            var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
            return Ok(result.Value);
        }

        // With a Patch, it's just a small 1 property update... 
        [HttpPatch("{id}")]
        public async Task<ActionResult<ArticleResponse>> TogglePublishArticle(int id)
        {
            var result = await _sender.Send(new TogglePublishArticleCommand { ArticleId = id });
            if (result.Failure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
