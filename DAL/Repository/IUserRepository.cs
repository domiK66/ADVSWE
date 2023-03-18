using DAL.Entities;

namespace DAL.Repository;
public interface IUserRepository: IRepository<User> {
    Task<User?> Login(String username, String password);
}