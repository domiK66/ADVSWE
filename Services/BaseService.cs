using DAL;
using DAL.Entities;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Models.Response;

namespace Services;
public abstract class BaseService<TEntity> where TEntity: Entity {
    protected UnitOfWork unitOfWork;
    protected IRepository<TEntity> repository;
    protected GlobalService globalService;
    protected IModelStateWrapper modelStateWrapper;
    protected ModelStateDictionary modelStateDictionary = null;
    protected User currentUser { get; private set; }

    protected Serilog.ILogger log = Utils.Logger.ContextLog<BaseService<TEntity>>();

    public async Task Load(String user) {
        currentUser = await unitOfWork.User.FindOneAsync(u => u.Email.ToLower().Equals(user.ToLower()));
    }
    public BaseService(UnitOfWork uow, IRepository<TEntity> repository, GlobalService service) {
        unitOfWork = uow;
        this.repository = repository;
        this.globalService = service;
    }

    public virtual async Task<ActionResponse> Delete(String id) {
        await repository.DeleteByIdAsync(id);
        var returnval = new ActionResponse();
        returnval.Success = true;
        return returnval;
    }

    public abstract Task<ItemResponse<TEntity>> Create(TEntity entity);
    public abstract Task<ItemResponse<TEntity>> Update(String id, TEntity entity);
    public abstract Task<Boolean> Validate(TEntity entity);

    public virtual async Task<ItemResponse<TEntity>> CreateHandler(TEntity entity) {
        ItemResponse<TEntity> response = new ItemResponse<TEntity>();
        if (await Validate(entity)){
            ItemResponse<TEntity> ent = await Create(entity);
            if (ent != null) {
                return ent;
            } else {
                response.HasError = true;
                response.Data = null;
                response.ErrorMessages.Add("Object was empty");
            }
        } else {
            response.HasError = true;
            response.ErrorMessages =  modelStateWrapper.Errors.Values.ToList();
        }
        return response;
    }

    public virtual async Task<ItemResponse<TEntity>> UpdateHandler(String id, TEntity entity) {
        ItemResponse<TEntity> response = new ItemResponse<TEntity>();
        if (await Validate(entity)){
            ItemResponse<TEntity> ent = await Update(id, entity);
            if (ent != null) {
                if (ent.HasError == false) {
                    ent.Data.ID = id;
                    await this.repository.UpdateOneAsync(ent.Data);
                    response.Data = ent.Data;
                    response.HasError = false;
                } else {
                    return ent;
                }
            } else {
                response.HasError = true;
                response.Data = null;
                response.ErrorMessages.Add("Object was empty");
            }
        } else {
            response.HasError = true;
            response.ErrorMessages =  modelStateWrapper.Errors.Values.ToList();
        }
        return response;
    }

    public virtual async Task<TEntity> Get(String id) {
        return await repository.FindByIdAsync(id);
    }

    public async virtual Task<List<TEntity>> Get() {
        return this.repository.FilterBy(x => true).ToList();
    }

    public async Task SetModelState(ModelStateDictionary validate){
        modelStateWrapper = new ModelStateWrapper(validate);
        this.modelStateDictionary = validate;
    }
}
