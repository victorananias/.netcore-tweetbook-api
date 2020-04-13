using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagsService.GetAllAsync();
            var response= tags.Select(t => new TagResponse {Name = t.Name});
            return Ok();
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var existingTag = await _tagsService.GetTagByNameAsync(request.Name);

            if (existingTag != null)
            {
                return Conflict();
            }

            var tag = new Tag
            {
                Name = request.Name,
                CreatorId = User.Claims.First(c => c.Type == "id").Value,
                CreatedOn = DateTime.UtcNow
            };

            var created = await _tagsService.CreateAsync(tag);

            if (!created)
            {
                return BadRequest();
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = $"{baseUrl}{ApiRoutes.Tags.GetAll}";

            return Created(
                locationUrl,
                new CreateTagResponse
                {
                    Name = tag.Name,
                    CreatedOn = tag.CreatedOn,
                    CreatorId = tag.CreatorId
                }
            );
        }
    }
}