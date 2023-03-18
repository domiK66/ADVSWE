using Logger = Utils.Logger;
using ILogger = Serilog.ILogger;

namespace Tests;
public class BaseUnitTest {
    protected ILogger log = Logger.ContextLog<BaseUnitTest>();
    [OneTimeSetUp] public async Task Setup() {
        Logger.InitLogger();
    }
}