using DAL;

namespace Services;

public class GlobalService {
    public UserService UserService {get; set;}

    public GlobalService(IUnitOfWork uow) {
        UnitOfWork uowi = (UnitOfWork)uow;
        UserService = new UserService(uowi, uowi.User, this);
    }
 }
