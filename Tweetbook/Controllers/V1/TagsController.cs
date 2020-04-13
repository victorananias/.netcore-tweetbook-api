using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TagsController(ITagsService tagsService, IMapper mapper)
        {
            _tagsService = tagsService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagsService.GetAllAsync();
            var response = _mapper.Map<List<TagResponse>>(tags);
            return Ok(response);
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

            var response = _mapper.Map<TagResponse>(tag);

            return Created(
                locationUrl,
                response
            );
        }
    }
}