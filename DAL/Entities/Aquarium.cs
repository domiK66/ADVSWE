namespace DAL.Entities {
    public class Aquarium: Entity {
        public string Name { get; set; }
        public Double Depth { get; set; }
        public Double Length { get; set; }
        public Double Height {get; set;}
        public Double Liters {get; set;}
    }
}