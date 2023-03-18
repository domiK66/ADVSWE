using DAL.Entities;

namespace DAL.Repository;
public class AquariumItemRepository: Repository<AquariumItem>, IAquariumItemRepository {
    public AquariumItemRepository(DBContext context): base(context) { }
    public List<Animal> GetAnimals(){
        return FilterByType<Animal>().ToList();
    }
    public List<Coral> GetCorals(){
        return FilterByType<Coral>().ToList();
    }
    public IEnumerable<E> FilterByType<E>() where E: AquariumItem {
        return FilterBy(x => true).OfType<E>();
    }
}