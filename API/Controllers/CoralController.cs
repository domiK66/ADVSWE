using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<Coral>>> Create([FromBody] Coral request)
        {
            return await CoralService.AddCoral(request);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<List<Coral>>>> Get()
        {
            return await CoralService.GetCorals();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<AquariumItem>>> Edit(
            [FromBody] AquariumItem request
        )
        {
            return await CoralService.Update(request.ID, request);
        }
    }
}
