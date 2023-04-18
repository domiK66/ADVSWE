using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Request;
using Services.Models.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : BaseController<Picture>
    {
        PictureService PictureService { get; set; }

        public PictureController(GlobalService service, IHttpContextAccessor accessor)
            : base(service.PictureService, accessor)
        {
            PictureService = service.PictureService;
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse<PictureResponse>>> Create(
            [FromBody] PictureRequest request
        )
        {
            return await PictureService.AddPicture(request.AquariumID, request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResponse>> Delete(String id)
        {
            return await PictureService.Delete(id);
        }
    }
}
