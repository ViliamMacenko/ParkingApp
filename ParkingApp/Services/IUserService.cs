using ParkingApp.Models.DTOs;

namespace ParkingApp.Services
{
    public interface IUserService
    {
        public Task<bool> Register(RegisterDTO registerDTO);
        public Task<string> Login(LoginDTO loginDTO);
        public bool validateUsername(string username);
        public bool validatePassword(string password);
        public bool validateName(string name);
        public string CreateSalt();
        public string HashPassword(string password, string salt);

    }
}
