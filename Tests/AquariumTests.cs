using DAL;
using DAL.Entities;

namespace Tests;
public class AquariumTests {
    UnitOfWork uow = new UnitOfWork();
    [Test] public async Task ShouldCreateAquarium() {
        Aquarium aquarium = new Aquarium() {
            Name = "Domis Aquarium",
            Depth = 65,
            Length = 150,
            Height = 55,
            Liters = 500,
            WaterType = WaterType.Saltwater
        };
        var createdAquarium = await uow.Aquarium.InsertOneAsync(aquarium);
        Assert.NotNull(createdAquarium);
        Assert.NotNull(createdAquarium.ID);
    }
    //TODO
    [Test] public async Task ShouldUpdateAquarium() {
        
    }

    [Test] public async Task ShouldDeleteAquarium() {
        
    }
}
