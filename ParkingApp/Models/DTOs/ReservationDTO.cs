namespace ParkingApp.Models.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; }
        public string SPZ { get; }
        public string OwnerName { get; }
        public DateTime Date { get; }

        public ReservationDTO(string sPZ, string ownerName, DateTime date, int id)
        {
            SPZ = sPZ;
            OwnerName = ownerName;
            Date = date;
            Id = id;
        }
    }
}
