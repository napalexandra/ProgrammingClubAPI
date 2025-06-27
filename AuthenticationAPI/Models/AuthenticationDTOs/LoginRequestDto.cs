using System.ComponentModel.DataAnnotations;

namespace AuhthenticationAPI.Models.AuthenticationDTOs
{
    public class LoginRequestDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
