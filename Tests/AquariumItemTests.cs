using DAL;
using DAL.Entities;
using DAL.Entities.Enums;

namespace Tests;

public class AquariumItemTests {
    UnitOfWork uow = new UnitOfWork();
    [Test] public async Task ShouldGetAquariumItemPerType() {
        var animal1 = new Animal(){
            Name = "Karpador",
            Species = "Wasserpokemon",
            Description = "XtestX"
        };
        var animal2 = new Animal(){
            Name = "Nemo",
            Species = "Clownfish",
            Description = "XtestX"
        };
        var coral1 = new Coral(){
            Name = "Corallo",
            Species = "Steinkoralle",
            CoralType = CoralType.HardCoral,
            Description = "XtestX"
        };
        var coral2 = new Coral(){
            Name = "Coralline",
            Species = "Lederkoralle",
            CoralType = CoralType.SoftCoral,
            Description = "XtestX"
        };
        Task.WaitAll(
            uow.AquariumItem.InsertOneAsync(animal1),
            uow.AquariumItem.InsertOneAsync(animal2),
            uow.AquariumItem.InsertOneAsync(coral1),
            uow.AquariumItem.InsertOneAsync(coral2)
        );
        var animals = uow.AquariumItem.GetAnimals();
        Assert.AreEqual(animals.Select(a => a.Description == "XtestX").Count(), 2);
        var corals = uow.AquariumItem.GetCorals();
        Assert.AreEqual(corals.Select(c => c.Description == "XtestX").Count(), 2);
        await uow.AquariumItem.DeleteManyAsync(i => i.Description == "XtestX");
    }
}