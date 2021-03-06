using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }        
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
    }
}