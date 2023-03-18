using DAL.Entities;

namespace DAL.Repository;
public class UserRepository: Repository<User>, IUserRepository {
    public UserRepository(DBContext context): base(context){
        passwordHasher = new BCPasswordHasher();
    }
    IPasswordHasher passwordHasher { get; }
    public async Task<User> Register(User user) {
        user.HashedPassword = passwordHasher.EncryptPassword(user.Password);
        //TODO: username
        await InsertOneAsync(user);
        return user;
    }
    public async Task<User?> Login(string username, string password) {
        var user = await FindOneAsync((user) => user.Username == username);
        if (user != null && passwordHasher.ValidatePassword(password, user.HashedPassword)) return user;
        return null;
    }
}