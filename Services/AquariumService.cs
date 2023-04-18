using DAL;
using DAL.Entities;
using DAL.Repository;
using Services.Models.Response;

namespace Services;

public class AquariumService : BaseService<Aquarium>
{
    public AquariumService(UnitOfWork uow, IRepository<Aquarium> repository, GlobalService service)
        : base(uow, repository, service) { }

    public async override Task<ItemResponse<Aquarium>> Create(Aquarium entity)
    {
        var response = new ItemResponse<Aquarium>()
        {
            Data = await repository.InsertOneAsync(entity),
        };
        return response;
    }

    public async override Task<ItemResponse<Aquarium>> Update(string id, Aquarium entity)
    {
        var response = new ItemResponse<Aquarium>()
        {
            Data = await repository.UpdateOneAsync(entity),
        };
        return response;
    }

    public async Task<ItemResponse<List<Aquarium>>> GetForUser(string userId)
    {
        var response = new ItemResponse<List<Aquarium>>();

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
