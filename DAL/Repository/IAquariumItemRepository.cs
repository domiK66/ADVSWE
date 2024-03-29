using DAL.Entities;

namespace DAL.Repository;
public interface IAquariumItemRepository: IRepository<AquariumItem> {
    List<Coral> GetCorals();
    List<Animal> GetAnimals();
}