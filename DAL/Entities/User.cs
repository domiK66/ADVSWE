using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DAL.Entities;
public class User: Entity {
    public String Username { get; set; }
    public String Email { get; set; }
    [JsonIgnore] [BsonIgnore] public String Password { get; set; }
    [JsonIgnore] public String HashedPassword { get; set; }
    public String Firstname { get; set; }
    public String Lastname { get; set; }
    public String Fullname { get => $"{Firstname} {Lastname}"; }
    public Boolean IsActive { get; set; }
}