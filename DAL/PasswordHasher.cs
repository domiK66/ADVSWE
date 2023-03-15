using BC = BCrypt.Net.BCrypt;

namespace DAL {
    public class PasswordHasher: IPasswordHasher {
        public string EncryptPassword(String password) {
            return BC.HashPassword(password, BC.GenerateSalt(12));
        }
        public bool ValidatePassword(String password, String hash) {
            return BC.Verify(password, hash);
        }
    }
}