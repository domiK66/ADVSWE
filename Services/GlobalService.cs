using DAL;

namespace Services;

public class GlobalService
{
    public UserService UserService { get; set; }
    public AquariumService AquariumService { get; set; }
    public CoralService CoralService { get; set; }
    public AnimalService AnimalService { get; set; }

    public PictureService PictureService { get; set; }

    public GlobalService(IUnitOfWork uow)
    {
        UnitOfWork uowi = (UnitOfWork)uow;
        UserService = new UserService(uowi, uowi.User, this);
        AquariumService = new AquariumService(uowi, uowi.Aquarium, this);
        CoralService = new CoralService(uowi, uowi.AquariumItem, this);
        AnimalService = new AnimalService(uowi, uowi.AquariumItem, this);
        PictureService = new PictureService(uowi, uowi.Picture, this);
    }
}
