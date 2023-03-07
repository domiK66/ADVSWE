namespace DAL.Entities {
    public class Animals: AquariumItem {
        public DateTime DeathDate {get; set; } = DateTime.MinValue;
        public Boolean IsAlive { get; set; }
    }
}
