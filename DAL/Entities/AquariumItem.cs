using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(Animal), typeof(Coral))]
//[JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
[KnownType(typeof(Animal))]
[KnownType(typeof(Coral))]
public abstract class AquariumItem: Entity {
    public AquariumItem(){ }
    public String Aquarium {get; set;}
    public String Name { get; set; }
    public String Species { get; set; }
    public DateTime Inserted { get; set; } = DateTime.Now;
    public int Amount { get; set; }
    public String Description { get; set; }
}