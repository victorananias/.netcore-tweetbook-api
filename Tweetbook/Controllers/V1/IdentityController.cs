using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly IIdentityService _identityService;

        public IdentityController(
            IIdentityService identityService
        )
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    }
                );
            }

            var authResult = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResult.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResult.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResult = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResult.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResult.Token
            });
        }
    }
}