using DAL.Entities;
using DAL.Repository;

namespace DAL;
public interface IUnitOfWork {
    DBContext Context { get; }
    IRepository<Aquarium> Aquarium { get; }
}