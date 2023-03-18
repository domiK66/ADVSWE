using System.Linq.Expressions;
using DAL.Entities;
using Logger = Utils.Logger;
using ILogger = Serilog.ILogger;
using MongoDB.Driver;

namespace DAL.Repository;
public class Repository<TEntity>: IRepository<TEntity> where TEntity: Entity {
    protected ILogger log = Logger.ContextLog<Repository<TEntity>>();
    private readonly IMongoCollection<TEntity> mongoCollection;
    public Repository(DBContext context) {
        mongoCollection = context.DataBase.GetCollection<TEntity>(typeof(TEntity).Name.ToString());
    }

    // https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e
    // Create
    public async Task<TEntity> InsertOneAsync(TEntity document) {
        document.ID = document.GenerateID();
        await mongoCollection.InsertOneAsync(document);
        return document;
    }
    
    // Read
    public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
        var entitites = await mongoCollection.FindAsync(filterExpression);
        return entitites.FirstOrDefault();
    }
    public Task<TEntity> FindByIdAsync(string id) {
        return FindOneAsync(doc => doc.ID == id);
    }
    public IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression) {
        return mongoCollection.Find(filterExpression).ToEnumerable();
    }
    public IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression
    ) {
        return mongoCollection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    // Update
    public async Task<TEntity> UpdateOneAsync(TEntity document) {
        await mongoCollection.FindOneAndReplaceAsync(doc => doc.ID == document.ID, document);
        return document;
    }
    public async Task<TEntity> InsertOrUpdateOneAsync(TEntity document) {
        var entity = await FindOneAsync(doc => doc.ID == document.ID);
        return await (entity == null ? InsertOneAsync(document) : UpdateOneAsync(document));
    }

    // Delete
    public async Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
        await mongoCollection.FindOneAndDeleteAsync(filterExpression);
    }
    public async Task DeleteByIdAsync(string id) {
        await DeleteOneAsync(doc => doc.ID == id);
    }
    public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression) {
        await mongoCollection.DeleteManyAsync(filterExpression);
    }
}