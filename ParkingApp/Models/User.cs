namespace ParkingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; }
        public List<Reservation> Reservations { get; set; } = null!;

        public User( string username, string password, string salt, string name)
        {
            Username = username;
            Password = password;
            Salt = salt;
            Name = name;
            Reservations = new List<Reservation>();
            Role = "User";
        }
    }
}
