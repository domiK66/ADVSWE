using DAL.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities;
public class Coral: AquariumItem {
    [BsonRepresentation(BsonType.String)] 
    public CoralType CoralType { get; set; }
}