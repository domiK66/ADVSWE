using DAL;
using DAL.Entities;
using DAL.Repository;
using Services.Models.Response;

namespace Services;

public class CoralService : AquariumItemService
{
    public CoralService(UnitOfWork uow, IRepository<AquariumItem> repository, GlobalService service)
        : base(uow, repository, service) { }

    public async Task<ItemResponseModel<Coral>> AddCoral(Coral coral)
    {
        var itemResponse = await AddAquariumItem(coral);

        var response = new ItemResponseModel<Coral>()
        {
            Data = itemResponse.Data as Coral,
            ErrorMessages = itemResponse.ErrorMessages,
        };
        return response;
    }

    public async Task<ItemResponseModel<List<Coral>>> GetCorals()
    {
        var response = new ItemResponseModel<List<Coral>>()
        {
            Data = unitOfWork.AquariumItem.GetCorals(),
        };
        return response;
    }
}