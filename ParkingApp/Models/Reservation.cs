namespace ParkingApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string SPZ { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public DateOnly Date { get; set; } 
        public User User { get; set; } = null!;

        public Reservation(string sPZ, string ownerName, DateOnly date,User user)
        {
            SPZ = sPZ;
            OwnerName = ownerName;
            Date = date;
            User = user;
        }
    }
}
