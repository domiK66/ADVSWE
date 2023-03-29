using DAL;
using DAL.Entities;
using DAL.Entities.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Services;

namespace Tests.ServiceTests;

public class AquariumServiceTests : BaseUnitTest
{
    UnitOfWork uow = new UnitOfWork();
    private AquariumService aquariumService;

    public AquariumServiceTests()
        : base()
    {
        aquariumService = new AquariumService(uow, uow.Aquarium, null);
    }

    [Test]
    public async Task ShouldGetAquariumsForUser()
    {
        var modelState = new Mock<ModelStateDictionary>();
        await aquariumService.SetModelState(modelState.Object);

        var user = await uow.User.Register(
            new User()
            {
                Firstname = "Mike",
                Lastname = "Kohl",
                Username = "shesh123",
                Email = "mike@marketing.com",
                Password = "Pa55w.rd",
                IsActive = true,
            }
        );

        var aquarium = await uow.Aquarium.InsertOneAsync(
            new Aquarium()
            {
                Liters = 300,
                Height = 55,
                Depth = 35,
                Length = 150,
                WaterType = WaterType.Saltwater,
                Name = "aquarium test 1",
            }
        );
        var aquarium2 = await uow.Aquarium.InsertOneAsync(
            new Aquarium()
            {
                Liters = 300,
                Height = 55,
                Depth = 35,
                Length = 150,
                WaterType = WaterType.Saltwater,
                Name = "aquarium test 2",
            }
        );

        await uow.UserAquarium.InsertOneAsync(
            new UserAquarium()
            {
                UserID = user.ID,
                AquariumID = aquarium.ID,
                Role = UserRole.Admin,
            }
        );
        await uow.UserAquarium.InsertOneAsync(
            new UserAquarium()
            {
                UserID = user.ID,
                AquariumID = aquarium2.ID,
                Role = UserRole.Admin,
            }
        );

        var response = await aquariumService.GetForUser(user.ID);

        Assert.NotNull(response);
        Assert.False(response.HasError);
        Assert.AreEqual(response.Data?.Count, 2);

        await uow.Aquarium.DeleteManyAsync(aqauarium => aqauarium.Name.StartsWith("aquarium test"));
        await uow.UserAquarium.DeleteManyAsync(item => item.UserID == user.ID);
        await uow.User.DeleteByIdAsync(user.ID);
    }
}
