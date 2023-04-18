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
    public async Task ShouldInsertUserAndNotTheSameTwice()
    {
        var user1 = new User()
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
        ItemResponse<User> fromservice = await userService.CreateHandler(user1);
        Assert.IsFalse(fromservice.HasError);

        var user2 = new User()
        {
            Firstname = "Dominik",
            Lastname = "Kainz",
            Username = "777dK",
            Password = "Pa55w.rd",
            Email = "kainz.domi@gmail.com",
            IsActive = true
        };
        ItemResponse<User> fromservice2 = await userService.CreateHandler(user2);
        Assert.IsTrue(fromservice2.HasError);
        await uow.User.DeleteManyAsync(user => user.Email == "kainz.domi@gmail.com");
    }

    [Test]
    public async Task ShouldUpdateUser()
    {
        var modelState = new Mock<ModelStateDictionary>();
        await userService.SetModelState(modelState.Object);

        var user = new User()
        {
            Firstname = "Dominik",
            Lastname = "Kainz",
            Username = "666dK1",
            Password = "Pa55w.rd",
            Email = "kainz.domi@gmail.com",
            IsActive = true
        };

        var userResponse = await userService.CreateHandler(user);
        userResponse.Data.Email = "kainz.domi@gmx.at";

        await userService.SetModelState(modelState.Object);

        ItemResponse<User> fromservice2 = await userService.UpdateHandler(
            userResponse.Data.ID,
            userResponse.Data
        );

        Assert.NotNull(fromservice2);
        Assert.IsFalse(fromservice2.HasError);
        await uow.User.DeleteManyAsync(user => user.Email == "kainz.domi@gmx.at");
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
