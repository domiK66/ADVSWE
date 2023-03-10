using Logger = Utils.Logger;
using ILogger = Serilog.ILogger;

namespace Tests {
    public class BaseUnitTests {
        protected ILogger log = Logger.ContextLog<BaseUnitTests>();

        [OneTimeSetUp]
        public async Task Setup() {
            Logger.InitLogger();
        }

        [Test]
        public void MyFirstLog() {
            log.Information("My first try");
            Assert.IsTrue(true);
        }
    }
}
