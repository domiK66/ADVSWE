using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoralController : BaseController<AquariumItem>
    {
        CoralService CoralService { get; set; }

        public CoralController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.CoralService, accessor)
        {
            CoralService = service.CoralService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemResponse<Coral>>> Create([FromBody] Coral request)
        {
            var result = await CoralService.AddCoral(request);
            if (result.HasError)
                return BadRequest(result);
            else
                return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ItemResponse<List<Coral>>>> Get()
        {
            return await CoralService.GetCorals();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemResponse<AquariumItem>>> Edit(
            [FromBody] AquariumItem request
        )
        {
            var result = await CoralService.Update(request.ID, request);
            if (result.HasError)
                return BadRequest(result);
            else
                return result;
        }
    }
}
