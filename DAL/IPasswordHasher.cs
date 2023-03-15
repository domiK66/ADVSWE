namespace DAL;

interface IPasswordHasher {
    public abstract string EncryptPassword(String password);
    public abstract bool ValidatePassword(String password, String hash);
}