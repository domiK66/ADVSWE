using DAL;
using DAL.Entities;
using DAL.Entities.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Services;

namespace Tests.ServiceTests;

public class CoralServiceTests : BaseUnitTest
{
    UnitOfWork uow = new UnitOfWork();
    private CoralService coralService;

    public CoralServiceTests()
        : base()
    {
        coralService = new CoralService(uow, uow.AquariumItem, null);
    }

    [Test]
    public async Task ShouldAddAnimal()
    {
        var modelState = new Mock<ModelStateDictionary>();
        await coralService.SetModelState(modelState.Object);

        var aquarium = await uow.Aquarium.InsertOneAsync(
            new Aquarium()
            {
                Liters = 500,
                Height = 55,
                Depth = 65,
                Length = 150,
                WaterType = WaterType.Saltwater,
                Name = "aquariumItem test",
            }
        );

        var coral = new Coral()
        {
            Name = "Cora",
            Amount = 12,
            Species = "Coral",
            CoralType = CoralType.SoftCoral,
            Aquarium = aquarium.ID,
            Description = "coral test"
        };

        var response = await coralService.AddCoral(coral);

        Assert.NotNull(response);
        Assert.False(response.HasError);
        Assert.NotNull(coral.Inserted);
        await uow.Aquarium.DeleteManyAsync(a => a.Name == "aquariumItem test");
        await uow.AquariumItem.DeleteManyAsync(item => item.Description == "coral test");
    }

    [Test]
    public async Task ShouldGetAnimals()
    {
        Task.WaitAll(
            uow.AquariumItem.InsertOneAsync(
                new Coral()
                {
                    Name = "Domi",
                    Amount = 1,
                    Species = "Coral",
                    CoralType = CoralType.SoftCoral,
                    Description = "coral test",
                }
            ),
            uow.AquariumItem.InsertOneAsync(
                new Coral()
                {
                    Name = "Paul",
                    Amount = 2,
                    Species = "Coral",
                    CoralType = CoralType.HardCoral,
                    Description = "coral test",
                }
            )
        );

        var response = await coralService.GetCorals();
        var animals = response.Data.Where(a => a.Description == "coral test");

        Assert.NotNull(response);
        Assert.False(response.HasError);
        Assert.AreEqual(animals.Count(), 2);
        await uow.AquariumItem.DeleteManyAsync(item => item.Description == "coral test");
    }
}
