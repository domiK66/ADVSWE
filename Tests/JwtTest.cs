using DAL;
using Services.Authentication;

namespace Tests;

public class JwtTest: BaseUnitTest {
        UnitOfWork uow = new UnitOfWork();
        [Test] public async Task Login() {
            var usr = await uow.User.Login("666dK","Pa55w.rd");

            Authentication auth = new Authentication(uow);
            AuthenticationInformation info = await auth.Authenticate(usr);
            //https://jwt.io/
            log.Debug($"{info}");
            Assert.NotNull(info);
        }
}
