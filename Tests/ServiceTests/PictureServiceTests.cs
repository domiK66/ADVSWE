using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Services;
using Services.Models.Request;
using Services.Models.Response;

namespace Tests.ServiceTests
{
    public class PictureServiceTests
    {
        [Test]
        public async Task UploadImage()
        {
            UnitOfWork uow = new UnitOfWork();
            PictureService service = new PictureService(uow, uow.Picture, null);

            PictureRequestModel request = new PictureRequestModel();
            request.Description = "sasdasd";

            byte[] bytes = System.IO.File.ReadAllBytes(
                "/Users/dominikkainz/Documents/ADVSWE/image.jpg"
            );

            IFormFile file = new FormFile(
                new MemoryStream(bytes),
                0,
                bytes.Length,
                "Data",
                "image.jpg"
            );

            request.FormFile = file;
            List<Picture> pictures = uow.Picture.FilterBy(x => true).ToList();
            int old = pictures.Count;
            await service.AddPicture("SchiScho", request);

            pictures = uow.Picture.FilterBy(x => true).ToList();
            Assert.AreEqual(pictures.Count, old + 1);
        }

        [Test]
        public async Task UploadImage2()
        {
            UnitOfWork uow = new UnitOfWork();
            PictureService service = new PictureService(uow, uow.Picture, null);

            PictureRequestModel request = new PictureRequestModel();
            request.Description = "sasdasd";

            byte[] bytes = System.IO.File.ReadAllBytes(
                "/Users/dominikkainz/Documents/ADVSWE/image.jpg"
            );

            IFormFile file = new FormFile(
                new MemoryStream(bytes),
                0,
                bytes.Length,
                "Data",
                "image.jpg"
            );

            request.FormFile = file;
            List<Picture> pictures = uow.Picture.FilterBy(x => true).ToList();
            int old = pictures.Count;
            ItemResponseModel<PictureResponseModel> pics = await service.AddPicture(
                "SchiScho",
                request
            );

            pictures = uow.Picture.FilterBy(x => true).ToList();
            Assert.AreEqual(pictures.Count, old + 1);
            Assert.IsFalse(pics.HasError);
        }
        /*
        [Test]
        public async Task FindPicture() {

            UnitOfWork uow = new UnitOfWork();
            PictureService service = new PictureService(uow, uow.Picture, null);

            var pic = await service.GetForAquarium("SchiScho");
            Assert.NotNull(pic);
        }
        */
    }
}