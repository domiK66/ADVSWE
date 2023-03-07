namespace DAL.Entities {
    public class Entity: IEntity {
        public String ID { get; set; }
        public String GenerateID() {
            throw new NotImplementedException();
        }
    }

}