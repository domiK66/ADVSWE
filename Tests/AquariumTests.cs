using DAL;
using DAL.Entities;

namespace Tests;
public class AquariumTests {
    UnitOfWork uow = new UnitOfWork();
    [Test] public async Task ShouldCreateAquarium() {
        Aquarium aquariumData = new Aquarium() {
            Name = "Domis Aquarium",
            Depth = 65,
            Length = 150,
            Height = 55,
            Liters = 500,
            WaterType = WaterType.Saltwater
        };
        var aquarium = await uow.Aquarium.InsertOneAsync(aquariumData);
        Assert.NotNull(aquarium);
        Assert.NotNull(aquarium.ID);
        await uow.Aquarium.DeleteOneAsync(doc => doc == aquarium);
    }
    //TODO
    [Test] public async Task ShouldGetAquariumByName() {
        Aquarium aquariumData = new Aquarium() {
            Name = "Mikes Aquarium",
            Depth = 70,
            Length = 150,
            Height = 70,
            Liters = 400,
            WaterType = WaterType.Freshwater
        };
        await uow.Aquarium.InsertOneAsync(aquariumData);
        var aquarium = await uow.Aquarium.GetByName("Mikes Aquarium");
        Assert.NotNull(aquarium);
        Assert.AreEqual(aquarium.Name, "Mikes Aquarium");
        Assert.AreEqual(aquarium.Liters, 400);
        await uow.Aquarium.DeleteByIdAsync(aquarium.ID);
    }

    [Test] public async Task ShouldUpdateAquarium() {
        Aquarium aquariumData = new Aquarium() {
            Name = "Ramons Aquarium",
            Depth = 40,
            Length = 150,
            Height = 60,
            Liters = 500,
            WaterType = WaterType.Saltwater
        };
        var aquarium = await uow.Aquarium.InsertOneAsync(aquariumData);
        Assert.NotNull(aquarium);
        aquarium.Height = 40;
        var updatedAquarium = await uow.Aquarium.UpdateOneAsync(aquarium);
        Assert.AreEqual(updatedAquarium.Height, 40);
        await uow.Aquarium.DeleteByIdAsync(aquarium.ID);
    }

    [Test] public async Task ShouldDeleteAquarium() {
        var aquarium = await uow.Aquarium.InsertOneAsync(new Aquarium() { Name = "Pauls Aquarium" });
        Assert.NotNull(aquarium.ID);
        await uow.Aquarium.DeleteByIdAsync(aquarium.ID);
        var deletedAquarium = await uow.Aquarium.FindByIdAsync(aquarium.ID);
        Assert.IsNull(deletedAquarium);
    }
}
