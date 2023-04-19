using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Request;
using Services.Models.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        UserService UserService { get; set; }

        public UserController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.UserService, accessor)
        {
            UserService = service.UserService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<UserResponse>>> Login(
            [FromBody] LoginRequest request
        )
        {
            return await UserService.Login(request);
        }
    }
}
