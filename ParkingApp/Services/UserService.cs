using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ParkingApp.Context;
using ParkingApp.Models;
using ParkingApp.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace ParkingApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _appContext;
        private readonly IAuthService _authService;
        public UserService(ApplicationContext appContext, IAuthService authService)
        {
            this._appContext = appContext;
            this._authService = authService;
        }
        public async Task<string> Login(LoginDTO loginDTO)
        {
            User user =await _appContext.Users.Where(u => u.Username.Equals(loginDTO.Username)).FirstOrDefaultAsync();
            if (user is null)
            {
                return null;
            }
            else
            {
                if (user.Password.Equals(HashPassword(loginDTO.Password, user.Salt)))
                {
                    return _authService.GenerateToken(user); 
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            if (validateUsername(registerDTO.Username))
            {
                if (validatePassword(registerDTO.Password))
                {
                    if (validateName(registerDTO.Name))
                    {
                        string salt = CreateSalt();
                        User user = new User(registerDTO.Username, HashPassword(registerDTO.Password, salt), salt, registerDTO.Name);
                        _appContext.Users.Add(user);
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
            else
            {
                return false;
            }
        }

        public bool validateUsername(string username)
        {
            return true;
        }
        public bool validatePassword(string username)
        {
            return true;
        }
        public bool validateName(string name)
        {
            return true;
        }
        public bool validateSPZ(string spz)
        {
            return true;
        }
        public string CreateSalt()
        {
            RNGCryptoServiceProvider saltShaker = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            saltShaker.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string HashPassword(string password, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(salt), 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            return Convert.ToBase64String(hash);
        }
    }
}
