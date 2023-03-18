using BC = BCrypt.Net.BCrypt;

namespace DAL;
public class BCPasswordHasher: IPasswordHasher {
    public string EncryptPassword(String password) {
        return BC.HashPassword(password);
    }
    public bool ValidatePassword(String password, String hash) {
        return BC.Verify(password, hash);
    }
}