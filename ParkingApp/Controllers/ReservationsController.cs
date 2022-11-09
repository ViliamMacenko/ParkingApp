using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApp.Models.DTOs;
using ParkingApp.Services;

namespace ParkingApp.Controllers
{
    [Authorize(Roles = "admin ,user")]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IAuthService authService;

        public ReservationsController(IReservationService reservationService, IAuthService authService)
        {
            this._reservationService = reservationService;
            this.authService = authService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<ReservationDTO> reservationDTOs = _reservationService.GetAll().Result;
            return Ok(reservationDTOs);
        }

        [HttpGet("")]
        public IActionResult GetForUser()
        {
            int id = authService.CheckCookies(Request.Cookies);
            if (id > 0)
            {
                List<ReservationDTO> reservationDTOs = _reservationService.GetForUser(id).Result;
                return Ok(reservationDTOs);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("")]
        public IActionResult Add(ReservationDTO reservationDTO)
        {
            int id = authService.CheckCookies(Request.Cookies);
            if (id > 0)
            {
                _reservationService.AddReservation(reservationDTO, id);
                return Ok(null);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int reservationId)
        {
            int userId = authService.CheckCookies(Request.Cookies);
            if (userId > 0)
            {
                _reservationService.RemoveReservation(reservationId, userId);
                return Ok(null);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
