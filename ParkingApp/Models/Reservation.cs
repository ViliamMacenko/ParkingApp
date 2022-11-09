namespace ParkingApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string SPZ { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public DateTime Date { get; set; } 
        public User User { get; set; } = null!;
        public int UserId { get; set; }

        public Reservation(string sPZ, string ownerName, DateTime date,User user)
        {
            SPZ = sPZ;
            OwnerName = ownerName;
            Date = date;
            User = user;
            UserId = User.Id;
        }

        public Reservation() 
        {
        }
    }
}
