using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.v1
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postsService.GetAllAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = await _postsService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }


        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post() { Name = postRequest.Name };

            await _postsService.CreateAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get}".Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id, Name = post.Name };

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postsService.UpdatePostAsync(post);

            if (!updated)
            {
                return NotFound();
            }
            
            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            if (!await _postsService.DeletePostAsync(postId))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}