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
        public IActionResult GetAll()
        {
            return Ok(_postsService.GetAll());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]Guid postId)
        {
            var post = _postsService.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = _postsService.UpdatePost(post);

            if (!updated)
            {
                return NotFound();
            }
            
            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post() {Id = postRequest.Id, Name = postRequest.Name};

            if (post.Id == Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }
            
            _postsService.GetAll().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get}".Replace("{postId}", post.Id.ToString());

            var response = new PostResponse {Id = post.Id, Name = post.Name};

            return Created(locationUrl, response);
        }
    }
}