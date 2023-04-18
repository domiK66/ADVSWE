using DAL;
using DAL.Entities;
using DAL.Repository;
using MimeKit;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Services.Models.Request;
using Services.Models.Response;

namespace Services;

public class PictureService : BaseService<Picture>
{
    public PictureService(UnitOfWork uow, IRepository<Picture> repository, GlobalService service)
        : base(uow, repository, service) { }

    public override Task<Models.Response.ItemResponse<Picture>> Create(Picture entity)
    {
        throw new NotImplementedException();
    }

    public override Task<Models.Response.ItemResponse<Picture>> Update(string id, Picture entity)
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

    public async Task<ItemResponse<PictureResponse>> AddPicture(
        string aqauariumId,
        PictureRequest request
    )
    {
        ItemResponse<PictureResponse> returnmodel = new ItemResponse<PictureResponse>();
        returnmodel.Data = null;
        returnmodel.HasError = true;

        if (request.FormFile != null)
        {
            String filename = request.FormFile.FileName;
            if (!String.IsNullOrEmpty(filename))
            {
                String typ = MimeTypes.GetMimeType(filename);
                if (typ.StartsWith("image/"))
                {
                    byte[] binaries = null;
                    using (var stream = new MemoryStream())
                    {
                        request.FormFile.CopyTo(stream);
                        binaries = stream.ToArray();
                    }
                    ObjectId pictureId = await unitOfWork.Context.GridFSBucket.UploadFromBytesAsync(
                        filename,
                        binaries
                    );
                    Picture pic = new Picture();
                    pic.PictureID = pictureId.ToString();
                    pic.Description = request.Description;
                    pic.AquariumID = aqauariumId;
                    pic.ContentType = typ;
                    Picture indb = await unitOfWork.Picture.InsertOneAsync(pic);

                    var bytes = await unitOfWork.Context.GridFSBucket.DownloadAsBytesAsync(
                        pictureId
                    );
                    PictureResponse response = new PictureResponse();
                    response.Picture = indb;
                    response.Bytes = bytes;
                    returnmodel.Data = response;
                    returnmodel.HasError = false;
                }
                else
                {
                    returnmodel.ErrorMessages.Add("Only images are allowed");
                }
            }
            else
            {
                returnmodel.ErrorMessages.Add("filename is empty");
            }
        }
        else
        {
            returnmodel.ErrorMessages.Add("No picture provided");
        }

        return returnmodel;
    }

    public async Task<ItemResponse<PictureResponse>> Get(string id)
    {
        var response = new ItemResponse<PictureResponse>();

        var picture = await repository.FindByIdAsync(id);

        if (picture == null)
        {
            response.ErrorMessages.Add("Picture is null");
            return response;
        }
        ;

        var pictureBytes = await unitOfWork.Context.GridFSBucket.DownloadAsBytesAsync(
            picture.PictureID
        );

        response.Data = new PictureResponse() { Picture = picture, Bytes = pictureBytes, };

        return response;
    }

    public async Task<ItemResponse<List<PictureResponse>>> GetForAquarium(string aquariumId)
    {
        var response = new ItemResponse<List<PictureResponse>>();

        var pictures = repository.FilterBy(picture => picture.AquariumID == aquariumId).ToList();

        pictures.ForEach(async picture =>
        {
            var pictureResponse = await Get(picture.ID);

            if (pictureResponse.ErrorMessages.Count > 0)
            {
                response.ErrorMessages.AddRange(pictureResponse.ErrorMessages);
            }

            response.Data.Add(pictureResponse.Data);
        });

        return response;
    }

    public override async Task<ActionResponse> Delete(string pictureId)
    {
        var picture = await repository.FindByIdAsync(pictureId);
        try
        {
            await unitOfWork.Context.GridFSBucket.DeleteAsync(picture.PictureID);
        }
        catch (Exception ex)
        {
            if (ex is GridFSFileNotFoundException)
            {
                log.Warning($"PictureID {picture.PictureID} not found in GridFS");
            }
            else
            {
                return new ActionResponse()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { ex.Message },
                };
            }
        }

        return await base.Delete(pictureId);
    }
}
