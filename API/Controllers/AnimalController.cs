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
    public class AnimalController : BaseController<AquariumItem>
    {
        AnimalService AnimalService { get; set; }

        public AnimalController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.AnimalService, accessor)
        {
            AnimalService = service.AnimalService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemResponse<Animal>>> Create([FromBody] Animal request)
        {
            var result = await AnimalService.AddAnimal(request);
            if (result.HasError)
                return BadRequest(result);
            else
                return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<List<Animal>>>> Get()
        {
            return await AnimalService.GetAnimals();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<AquariumItem>>> Edit(
            [FromBody] AquariumItem request
        )
        {
            var result = await AnimalService.Update(request.ID, request);
            if (result.HasError)
                return BadRequest(result);
            else
                return result;
        }
    }
}
