namespace DAL.Entities {
    public interface IEntity {
        public String ID { get; set; }
        public string GenerateID ();
    }
}