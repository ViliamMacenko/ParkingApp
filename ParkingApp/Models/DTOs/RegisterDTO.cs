namespace ParkingApp.Models.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; } 
        public string Password { get; } 
        public string Name { get; } 

        public RegisterDTO(string username, string password, string name)
        {
            Username = username;
            Password = password;
            Name = name;
        }
    }
}
