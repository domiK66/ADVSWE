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
    public class AquariumController : BaseController<Aquarium>
    {
        AquariumService AquariumService { get; set; }

        public AquariumController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.AquariumService, accessor)
        {
            AquariumService = service.AquariumService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<Aquarium>>> Create([FromBody] Aquarium request)
        {
            return await AquariumService.Create(request);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<Aquarium>>> Edit([FromBody] Aquarium request)
        {
            return await AquariumService.Update(request.ID, request);
        }

        [HttpGet("ForUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<List<Aquarium>>>> ForUser(String id)
        {
            return await AquariumService.GetForUser(id);
        }
    }
}
