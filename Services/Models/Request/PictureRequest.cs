using Microsoft.AspNetCore.Http;

namespace Services.Models.Request
{
    public class PictureRequest
    {
        public String Description { get; set; }
        public IFormFile FormFile { get; set; }
        public String PictureID { get; set; }
        public String AquariumID { get; set; }
    }
}
