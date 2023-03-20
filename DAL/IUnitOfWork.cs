using DAL.Entities;
using DAL.Repository;

namespace DAL;
public interface IUnitOfWork {
    DBContext Context { get; }
    IAquariumRepository Aquarium { get; }
    IAquariumItemRepository AquariumItem { get; }
    IUserRepository User { get; }
}