using Microsoft.EntityFrameworkCore;
using ParkingApp.Context;
using ParkingApp.Models;
using ParkingApp.Models.DTOs;

namespace ParkingApp.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationContext _appContext;

        public ReservationService(ApplicationContext appContext)
        {
            this._appContext = appContext;
        }

        public async Task<bool> AddReservation(ReservationDTO reservationDTO, int id)
        {
            if (ValidateDate(reservationDTO.Date))
            {
                if (ValidateSPZ(reservationDTO.SPZ))
                {
                    User user = await _appContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                    Reservation reseravation = new Reservation(reservationDTO.SPZ, user.Name, reservationDTO.Date, user);
                    _appContext.Reservations.Add(reseravation);
                    await _appContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ReservationDTO>> GetAll()
        {
            List<Reservation> reservations = await _appContext.Reservations.ToListAsync();
            return ReservationsToDTOs(reservations);
        }

        public async Task<List<ReservationDTO>> GetForUser(int id) 
        {
            List<Reservation> reservations = await _appContext.Reservations.Include(r=>r.User).Where(r=>r.User.Id==id).ToListAsync();
            return ReservationsToDTOs(reservations);
        }

        public async Task<bool> RemoveReservation(int reservationId, int userId)
        {
            Reservation reservation = await _appContext.Reservations.Include(r=>r.User).Where(r => r.Id == reservationId).FirstOrDefaultAsync();
            if (reservation is not null && reservation.User.Id==userId)
            {
                _appContext.Reservations.Remove(reservation);
                await _appContext.SaveChangesAsync();
                return true;
            }            
            else 
            {
                return false;
            }
        }

        public bool ValidateSPZ(string sPZ)
        {
            return true;
        }

        public bool ValidateDate(DateOnly date)
        {
            return true;
        }

        public List<ReservationDTO> ReservationsToDTOs(List<Reservation> reservations) 
        {
            if (reservations.Count > 0)
            {
                List<ReservationDTO> reservationsList = new List<ReservationDTO>();
                foreach (Reservation reservation in reservations)
                {
                    ReservationDTO reservationDTO = new ReservationDTO(reservation.SPZ, reservation.OwnerName, reservation.Date, reservation.Id);
                    reservationsList.Add(reservationDTO);
                }
                return reservationsList;
            }
            else return new List<ReservationDTO>();
        }
    }
}
