using DAL;
using DAL.Entities;

namespace Tests;

public class UserTests {
    UnitOfWork uow = new UnitOfWork();
    [Test] public async Task RegisterAndLoginTest(){
        var user = new User(){
            Firstname = "Dominik",
            Lastname = "Kainz",
            Username = "666dK",
            Password = "Pa55w.rd"
        };
        var registeredUser = await uow.User.Register(user);
        Assert.AreEqual(registeredUser.Username, "666dK");
        var loggedInUser = await uow.User.Login("666dK", "Pa55w.rd");
        Assert.NotNull(loggedInUser.ID);
        Assert.AreEqual(loggedInUser.Firstname, "Dominik");
        await uow.User.DeleteByIdAsync(loggedInUser.ID);
    }
}