using DAL;

namespace Tests;

class DBConnectionTest: BaseUnitTest {
    [Test] public async Task TestDBConnection() {
        UnitOfWork uow = new UnitOfWork();
        Assert.IsTrue(uow.Context.IsConnected);
    }
}
