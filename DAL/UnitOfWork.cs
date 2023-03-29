using DAL.Entities;
using DAL.Repository;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork()
    {
        Context = new DBContext();
    }

    public DBContext Context { get; private set; }

    // public IRepository<Aquarium> Aquarium => new Repository<Aquarium>(Context);
    public IAquariumRepository Aquarium => new AquariumRepository(Context);
    public IAquariumItemRepository AquariumItem => new AquariumItemRepository(Context);
    public IUserRepository User => new UserRepository(Context);
    public IRepository<UserAquarium> UserAquarium => new Repository<UserAquarium>(Context);
    public IRepository<Picture> Picture => new Repository<Picture>(Context);
}
