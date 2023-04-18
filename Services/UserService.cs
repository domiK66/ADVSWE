using DAL;
using DAL.Entities;
using DAL.Repository;
using Services.Auth;
using Services.Models.Request;
using Services.Models.Response;

namespace Services;

public class UserService : BaseService<User>
{
    public UserService(UnitOfWork uow, IRepository<User> repository, GlobalService service)
        : base(uow, repository, service) { }

    public override async Task<ItemResponse<User>> Create(User entity)
    {
        ItemResponse<User> response = new ItemResponse<User>();
        response.Data = await this.repository.InsertOneAsync(entity);
        response.HasError = false;
        return response;
    }

    public override async Task<ItemResponse<User>> Update(string id, User entity)
    {
        ItemResponse<User> response = new ItemResponse<User>();
        response.Data = await this.repository.UpdateOneAsync(entity);
        response.HasError = false;
        return response;
    }

    public async Task<ItemResponse<UserResponse>> Login(LoginRequest request)
    {
        var response = new ItemResponse<UserResponse>();
        var user = await unitOfWork.User.Login(request.Username, request.Password);

        if (user == null)
        {
            response.ErrorMessages.Add("Username and/or password incorrect");
            return response;
        }

        var auth = new Authentication(unitOfWork);
        var authInfo = await auth.Authenticate(user);

        if (authInfo == null)
        {
            response.ErrorMessages.Add("Authentication failed");
            return response;
        }

        response.Data = new UserResponse() { AuthInfo = authInfo, User = user };

        return response;
    }

    public override async Task<bool> Validate(User entity)
    {
        if (entity != null)
        {
            if (
                this.repository.FilterBy(e => e.ID == entity.ID).Count() == 0
                && this.repository.FilterBy(e => e.Email == entity.Email).Count() >= 1
            )
            {
                modelStateWrapper.AddError("Email already exists", "This email is already taken!");
            }
            if (String.IsNullOrEmpty(entity.Email))
            {
                modelStateWrapper.AddError("No Email", "Please provide an email!");
            }
            if (String.IsNullOrEmpty(entity.Firstname))
            {
                modelStateWrapper.AddError("No Firstname", "Please provide an firstname!");
            }
            if (String.IsNullOrEmpty(entity.Lastname))
            {
                modelStateWrapper.AddError("No Lastname", "Please provide an lastname!");
            }
        }
        else
        {
            modelStateWrapper.AddError("No user", "no user was provided");
        }
        return modelStateWrapper.IsValid;
    }
}
