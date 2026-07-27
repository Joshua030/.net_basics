using Application.Features.Identity.Tokens;
using Application.Features.Identity.Tokens.Queries;
using Infrastructure.Constants;
using Infrastructure.Identity.Auth;
using Infrastructure.OpenApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ABCSharedLibrary.Constants;
using ABCSharedLibrary.Models.Requests.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        [TenantHeader]
        [OpenApiOperation("Used to obtai jwt for login.")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest tokenRequest)
        {
            var response = await Sender.Send(new GetTokenQuery { TokenRequest = tokenRequest });

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("refresh-token")]
        [OpenApiOperation("Used to generate jwt from refresh token.")]
        [ShouldHavePermission(action: SchoolAction.RefreshToken, feature: SchoolFeature.Tokens)]
        public async Task<IActionResult> GetRefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var response = await Sender.Send(new GetRefreshTokenQuery { RefreshToken = refreshTokenRequest });

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
