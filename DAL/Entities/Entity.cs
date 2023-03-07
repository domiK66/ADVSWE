namespace DAL.Entities {
    public class Entity: IEntity {
        public string ID { get; set;}
        public string GenerateID () {
            throw new NotImplementedException();
        }
    }

}