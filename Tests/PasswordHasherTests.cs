using DAL;

namespace Tests;
class PasswordHasherTests: BaseUnitTest {
    [Test] public void EncryptAndValidatePassword() {
        var hasher = new BCPasswordHasher();
        string testPassword = "Dosenbier@Home8010!";
        var hashedPassword = hasher.EncryptPassword(testPassword);
        log.Information($"{hashedPassword}");
        var validated = hasher.ValidatePassword(testPassword, hashedPassword);
        log.Information($"{validated}");
        Assert.IsTrue(validated);
    }
}