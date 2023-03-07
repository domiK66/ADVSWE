namespace DAL.Entities {
    class UserAquarium: Entity {
        public String UserID { get; set; }
        public String AquariumID { get; set; }
        public UserRole Role { get; set; }
    }
    public enum UserRole { User, Admin }
}