using Microsoft.AspNetCore.Http;

namespace Services.Models.Request
{
    public class PictureRequestModel
    {
        public String Description {get; set;}
        public IFormFile FormFile { get; set; }
    }
}