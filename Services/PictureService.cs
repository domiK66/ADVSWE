using DAL;
using DAL.Entities;
using DAL.Repository;
using MimeKit;
using MongoDB.Bson;
using Services.Models.Request;
using Services.Models.Response;

namespace Services;

public class PictureService : BaseService<Picture>
{
    public PictureService(UnitOfWork uow, IRepository<Picture> repository, GlobalService service)
        : base(uow, repository, service) { }

    public override Task<Models.Response.ItemResponseModel<Picture>> Create(Picture entity)
    {
        throw new NotImplementedException();
    }

    public override Task<Models.Response.ItemResponseModel<Picture>> Update(
        string id,
        Picture entity
    )
    {
        throw new NotImplementedException();
    }

    public override async Task<bool> Validate(Picture entity)
    {
        if (entity == null)
        {
            modelStateWrapper.AddError("No picture", "no picture was provided");
        }
        return modelStateWrapper.IsValid;
    }

    public async Task<ItemResponseModel<PictureResponseModel>> AddPicture(
        String aqauarium,
        PictureRequestModel request
    )
    {
        ItemResponseModel<PictureResponseModel> returnmodel = new ItemResponseModel<PictureResponseModel>();
        returnmodel.Data = null;
        returnmodel.HasError = true;

        if (request.FormFile != null) {
            String filename = request.FormFile.FileName;
            if (!String.IsNullOrEmpty(filename)){
                String typ = MimeTypes.GetMimeType(filename);
                if (typ.StartsWith("image/")) {
                    byte[] binaries = null;
                    using (var stream = new MemoryStream()) {
                        request.FormFile.CopyTo(stream);
                        binaries = stream.ToArray();
                    }
                    ObjectId pictureId = await unitOfWork.Context.GridFSBucket.UploadFromBytesAsync(filename, binaries);
                    Picture pic = new Picture();
                    pic.PictureID = pictureId.ToString();
                    pic.Description = request.Description;
                    pic.Aquarium = aqauarium;
                    pic.ContentType = typ;
                    Picture indb = await unitOfWork.Picture.InsertOneAsync(pic);

                    var bytes = await unitOfWork.Context.GridFSBucket.DownloadAsBytesAsync(pictureId);
                    PictureResponseModel response = new PictureResponseModel();
                    response.Picture = indb;
                    response.Bytes = bytes;
                    returnmodel.Data = response;
                    returnmodel.HasError = false;
                } else {
                    returnmodel.ErrorMessages.Add("Only images are allowed");
                }
            } else {
                returnmodel.ErrorMessages.Add("filename is empty");
            }
        } else {
            returnmodel.ErrorMessages.Add("No picture provided");
        }

        return returnmodel;
    }
    // TODO
    /* public async Task<ItemResponseModel<PictureResponseModel>> Delete(String id) {

    }
    

    public async Task<ItemResponseModel<List<PictureResponseModel>>> GetForAquarium(String aquarium) {
        var response = new ItemResponseModel<List<PictureResponseModel>>();
        if (String.IsNullOrEmpty(aquarium))
        {
            response.ErrorMessages.Add("No aquarium provided!");
            return response;
        }
        var aquariumPictures = unitOfWork.Picture.FilterBy(a => a.Aquarium == aquarium);

        response.Data = aquariumPictures.ToList();
        return response;
    }


    public async Task<ItemResponseModel<PictureResponseModel>> GetPicture(String id) {
        var picture = unitOfWork.Picture.FindByIdAsync(id);
        var response = new ItemResponseModel<PictureResponseModel>() {
            Data = picture };

        return response;
    }
    */
    
}
