using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Services;
using Services.Models.Response;

namespace Tests;

public class UserServiceTests : BaseUnitTest
{
    UnitOfWork uow = new UnitOfWork();

    private UserService userService;

    public UserServiceTests()
        : base()
    {
        userService = new UserService(uow, uow.User, null);
    }

    [Test]
    public async Task TestInsert()
    {
        var user = new User()
        {
            Firstname = "Dominik",
            Lastname = "Kainz",
            Username = "666dK",
            Password = "Pa55w.rd",
            Email = "kainz.domi@gmail.com",
            IsActive = true
        };
        var modelState = new Mock<ModelStateDictionary>();
        await userService.SetModelState(modelState.Object);

        ItemResponseModel<User> fromservice = await userService.CreateHandler(user);

        Assert.IsTrue(fromservice.HasError);
    }

    [Test]
    public async Task TestUpdate()
    {
        var user = await uow.User.Login("666dK1", "Pa55w.rd");
        user.Email = "kainz.domi@gmx.at";
        var modelState = new Mock<ModelStateDictionary>();
        await userService.SetModelState(modelState.Object);

        ItemResponseModel<User> fromservice = await userService.UpdateHandler(user.ID, user);

        Assert.NotNull(fromservice);
        Assert.IsFalse(fromservice.HasError);
    }

    [Test]
    public async Task ShouldLoginAndAuthenticateUser()
    {
        var modelState = new Mock<ModelStateDictionary>();
        await userService.SetModelState(modelState.Object);

        var user = await uow.User.Register(
            new User()
            {
                Firstname = "Dominik",
                Lastname = "Kainz",
                Username = "666dK2",
                Email = "domi@kainz.com",
                Password = "Pa55w.rd",
                IsActive = true,
            }
        );

        var loginResponse = await userService.Login(
            new() { Username = "666dK2", Password = "Pa55w.rd", }
        );

        Assert.NotNull(loginResponse);
        Assert.False(loginResponse.HasError);

        Assert.NotNull(loginResponse.Data?.AuthInfo);
        Assert.IsNotEmpty(loginResponse.Data?.AuthInfo.Token);


        await uow.User.DeleteManyAsync(user => user.Email == "domi@kainz.com");
    }
}
