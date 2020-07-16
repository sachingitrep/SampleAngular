using System;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<bool> UserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<User> Login(UserDto userDto)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == userDto.Username);
            if(user == null)
            return null;
            
            if(!VerfiyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

            return user;
        }

        private bool VerfiyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    return false;
                }
                return true;
            }            
        }

        public async Task<User> Register(UserDto userDto)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
            var user = new User {
                Username = userDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}