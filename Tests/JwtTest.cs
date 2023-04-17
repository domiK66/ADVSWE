using DAL;
using Services.Auth;

namespace Tests;

public class JwtTest : BaseUnitTest
{
    UnitOfWork uow = new UnitOfWork();

    [Test]
    public async Task Login()
    {
        var user = await uow.User.Login("666dK", "Pa55w.rd");

        Authentication auth = new Authentication(uow);
        AuthenticationInformation info = await auth.Authenticate(user);
        //https://jwt.io/
        log.Debug($"{info}");
        Assert.Null(info);
    }
}
