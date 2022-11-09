using ParkingApp.Models;
using ParkingApp.Models.DTOs;

namespace ParkingApp.Services
{
    public interface IReservationService
    {
        public Task<bool> AddReservation(ReservationDTO reservationDTO, int id);
        public Task<bool> RemoveReservation(int reservationId,int userId);
        public Task<List<ReservationDTO>> GetAll();
        public Task<List<ReservationDTO>> GetForUser(int id);

        public bool ValidateSPZ(string sPZ);
        public bool ValidateDate(DateTime date);
        public Task<Reservation> GetReservation(int reservationId, int userId);
        public Task<bool> UpdateDb();
        public Task<User> GetUser(int id);
    }
}
