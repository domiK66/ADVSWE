namespace Tests;
class LoggerTests: BaseUnitTest {
    [Test] public void FirstLog() {
        log.Information("The first log");
        Assert.IsTrue(true);
    }
}