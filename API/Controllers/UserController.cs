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
        public UserController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.UserService, accessor) { }

        [HttpPost]
        public async Task<ActionResult<ItemResponseModel<UserResponseModel>>> Login ([FromBody]LoginRequestModel request) {
            return UserService.Login();
        }
    } }

