using DAL;
using DAL.Entities;
using DAL.Repository;
using Services.Models.Response;

namespace Services;

public class AnimalService : AquariumItemService
{
    public AnimalService(
        UnitOfWork unit,
        IRepository<AquariumItem> repository,
        GlobalService service
    )
        : base(unit, repository, service) { }

    public async Task<ItemResponse<Animal>> AddAnimal(Animal animal)
    {
        var itemResponse = await AddAquariumItem(animal);

        var response = new ItemResponse<Animal>()
        {
            Data = itemResponse.Data as Animal,
            ErrorMessages = itemResponse.ErrorMessages,
            HasError = itemResponse.HasError
        };
        return response;
    }

    public async Task<ItemResponse<List<Animal>>> GetAnimals()
    {
        var response = new ItemResponse<List<Animal>>()
        {
            Data = unitOfWork.AquariumItem.GetAnimals(),
        };
        return response;
    }

    public async Task<bool> Validate(Animal animal)
    {
        await base.Validate(animal);

        if (animal.DeathDate != null)
        {
            modelStateWrapper.AddError("dead animal", "cant add");
        }

        return modelStateWrapper.IsValid;
    }
}
