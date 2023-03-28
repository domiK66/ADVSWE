using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Services;
using Services.Models.Response;

namespace Tests
{
    public class UserServiceTest: BaseUnitTest
    {
        UnitOfWork uow = new UnitOfWork();
        [Test] public async Task TestInsert() {
            UserService userService = new UserService(uow, uow.User, null);
            var user = new User(){
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

            Assert.NotNull(fromservice);
            Assert.IsFalse(fromservice.HasError);
        }

            [Test] public async Task TestUpdate() {
            UserService userService = new UserService(uow, uow.User, null);
            var usr = await uow.User.Login("666dK","Pa55w.rd");
            usr.Email = "kainz.domi@gmx.at";
            var modelState = new Mock<ModelStateDictionary>();
            await userService.SetModelState(modelState.Object);

            ItemResponseModel<User> fromservice = await userService.UpdateHandler(usr.ID, usr);

            Assert.NotNull(fromservice);
            Assert.IsFalse(fromservice.HasError);
        }


    }
}