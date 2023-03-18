using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities;
public abstract class Entity: IEntity {
    [BsonId] public String ID { get; set; }
    public String GenerateID() {
        return ObjectId.GenerateNewId().ToString();
    }
}