using Application.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController() { }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponseDTO>> GetMe(
            [FromHeader] string JWT)
        {
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(JWT);

            throw new NotImplementedException();

        }
    }
}
