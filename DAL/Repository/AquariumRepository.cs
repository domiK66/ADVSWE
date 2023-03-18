using DAL.Entities;

namespace DAL.Repository;
public class AquariumRepository: Repository<Aquarium>, IAquariumRepository {
    public AquariumRepository(DBContext context): base(context) { }
    public Task<Aquarium> GetByName(string name) {
        return FindOneAsync((aquarium) => aquarium.Name == name);
    }
}