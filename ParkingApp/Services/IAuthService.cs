using ParkingApp.Models;

namespace ParkingApp.Services
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
        public int ValidateToken(string token);
        public int CheckCookies(IRequestCookieCollection cookies);
    }
}
