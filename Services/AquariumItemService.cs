using DAL;
using DAL.Entities;
using DAL.Repository;
using Services;
using Services.Models.Response;

public class AquariumItemService : BaseService<AquariumItem>
{
    public AquariumItemService(
        UnitOfWork uow,
        IRepository<AquariumItem> repository,
        GlobalService service
    )
        : base(uow, repository, service) { }

    public Task<ItemResponseModel<AquariumItem>> AddAquariumItem(AquariumItem item)
    {
        return CreateHandler(item);
    }

    public async override Task<ItemResponseModel<AquariumItem>> Create(AquariumItem entity)
    {
        var response = new ItemResponseModel<AquariumItem>()
        {
            Data = await repository.InsertOneAsync(entity),
        };
        return response;
    }

    public async override Task<ItemResponseModel<AquariumItem>> Update(
        string id,
        AquariumItem entity
    )
    {
        var response = new ItemResponseModel<AquariumItem>()
        {
            Data = await repository.UpdateOneAsync(entity),
        };
        return response;
    }

    public override async Task<bool> Validate(AquariumItem item)
    {
        if (item == null)
        {
            modelStateWrapper.AddError("AquariumItem null", "Aquarium item is null");
            return modelStateWrapper.IsValid;
        }

        var aquarium = await unitOfWork.Aquarium.FindByIdAsync(item.Aquarium);
        if (aquarium == null)
        {
            modelStateWrapper.AddError("Aquarium does not exist", "No a valid aquarium");
        }
        if (item.Amount <= 0)
        {
            modelStateWrapper.AddError("Amount must be positive", "Amount must be positive");
        }

        return modelStateWrapper.IsValid;
    }
}
