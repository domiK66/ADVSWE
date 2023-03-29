using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using DAL;
using Services;

namespace Tests.ServiceTests;

public class AnimalServiceTests : BaseUnitTest
{
    UnitOfWork uow = new UnitOfWork();

    private AnimalService animalService;

    public AnimalServiceTests()
        : base()
    {
        animalService = new AnimalService(uow, uow.AquariumItem, null);
    }

    [Test]
    public async Task ShouldAddAnimal()
    {
        var modelState = new Mock<ModelStateDictionary>();
        await animalService.SetModelState(modelState.Object);

        var aquarium = await uow.Aquarium.InsertOneAsync(
            new Aquarium()
            {
                Liters = 300,
                Height = 25,
                Depth = 45,
                Length = 100,
                WaterType = WaterType.Saltwater,
                Name = "aquariumItem test",
            }
        );

        var animal = new Animal()
        {
            Name = "Nemo",
            Amount = 12,
            Species = "Clownfish",
            IsAlive = true,
            Aquarium = aquarium.ID,
            Description = "animal test"
        };

        var response = await animalService.AddAnimal(animal);

        Assert.NotNull(response);
        Assert.False(response.HasError);
        Assert.NotNull(animal.Inserted);
        await uow.Aquarium.DeleteManyAsync(a => a.Name == "aquariumItem test");
        await uow.AquariumItem.DeleteManyAsync(item => item.Description == "animal test");
    }

    [Test]
    public async Task ShouldGetAnimals()
    {
        Task.WaitAll(
            uow.AquariumItem.InsertOneAsync(
                new Animal()
                {
                    Name = "Nemo",
                    IsAlive = true,
                    Amount = 3,
                    Species = "Clownfish",
                    Description = "animal test",
                }
            ),
            uow.AquariumItem.InsertOneAsync(
                new Animal()
                {
                    Name = "Dorie",
                    IsAlive = true,
                    Amount = 4,
                    Species = "Surgeonfish",
                    Description = "animal test",
                }
            )
        );

        var response = await animalService.GetAnimals();
        var animals = response.Data.Where(a => a.Description == "animal test");

        Assert.NotNull(response);
        Assert.False(response.HasError);
        Assert.AreEqual(animals.Count(), 2);

        await uow.AquariumItem.DeleteManyAsync(item => item.Description == "animal test");
    }
}
