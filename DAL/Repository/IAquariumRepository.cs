using DAL.Entities;

namespace DAL.Repository; 
public interface IAquariumRepository: IRepository<Aquarium> {
    Task<Aquarium> GetByName(string name);
}