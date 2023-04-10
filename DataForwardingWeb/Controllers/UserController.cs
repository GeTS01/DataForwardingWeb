using DataForwardingWeb.DTO.Model;
using DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;

namespace DataForwardingWeb.Controllers
{
    public class UserController : BaseController
    {
        private readonly AuthService _userService;

        public UserController(AuthService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(typeof(SignInDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] AuthModel request)
        {
            var response = await _userService.Auth(request);
            return Ok(response);
        }
    }
}
