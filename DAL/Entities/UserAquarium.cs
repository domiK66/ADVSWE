using DAL.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities;
class UserAquarium: Entity {
    public String UserID { get; set; }
    public String AquariumID { get; set; }
    [BsonRepresentation(BsonType.String)] public UserRole Role { get; set; }
}