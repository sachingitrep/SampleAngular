using System.Threading.Tasks;
using DatingApp.API.Dtos;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(UserDto userDto);
        Task<User> Login(UserDto userDto);
        Task<bool> UserExists(string username);
    }
}