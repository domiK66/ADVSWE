using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemResponse<Animal>>> Create([FromBody] Animal request)
        {
            return await AnimalService.AddAnimal(request);
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
            return await AnimalService.Update(request.ID, request);
        }
    }
}
