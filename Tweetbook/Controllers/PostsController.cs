using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Domain;

namespace Tweetbook.Controllers
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();

            for (int i = 0; i <  6; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }


        [HttpGet("api/v1/posts")]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }
    }
}