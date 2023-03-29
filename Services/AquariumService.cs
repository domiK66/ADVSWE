using DAL;
using DAL.Entities;
using DAL.Repository;
using Services.Models.Response;

namespace Services;

public class AquariumService : BaseService<Aquarium>
{
    public AquariumService(UnitOfWork uow, IRepository<Aquarium> repository, GlobalService service)
        : base(uow, repository, service) { }

    public override Task<ItemResponseModel<Aquarium>> Create(Aquarium entity)
    {
        throw new NotImplementedException();
    }

    public override Task<ItemResponseModel<Aquarium>> Update(string id, Aquarium entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ItemResponseModel<List<Aquarium>>> GetForUser(string userId)
    {
        var response = new ItemResponseModel<List<Aquarium>>();

        if (String.IsNullOrEmpty(userId))
        {
            response.ErrorMessages.Add("No userID provided!");
            return response;
        }

        var userAquariums = unitOfWork.UserAquarium.FilterBy(ua => ua.UserID == userId);
        var aquariums = repository.FilterBy(
            aquarium => userAquariums.Any(ua => ua.AquariumID == aquarium.ID)
        );

        response.Data = aquariums.ToList();
        return response;
    }

    public override async Task<bool> Validate(Aquarium entity)
    {
        if (entity.Depth <= 0)
        {
            modelStateWrapper.AddError("Depth not positive", "Depth must be greater than 0");
        }
        if (entity.Height <= 0)
        {
            modelStateWrapper.AddError("Height not positive", "Height must be greater than 0");
        }
        if (entity.Length <= 0)
        {
            modelStateWrapper.AddError("Length not positive", "Length must be greater than 0");
        }
        if (entity.Liters <= 0)
        {
            modelStateWrapper.AddError("Liters not positive", "Liters must be greater than 0");
        }

        return modelStateWrapper.IsValid;
    }
}
