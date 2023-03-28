using DAL;

namespace Tests;

class DBConnectionTest : BaseUnitTest
{
    [Test]
    public void TestDBConnection()
    {
        UnitOfWork uow = new UnitOfWork();
        Assert.IsTrue(uow.Context.IsConnected);
    }
}
