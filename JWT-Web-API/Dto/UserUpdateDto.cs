using System.ComponentModel.DataAnnotations;

namespace JWTWebAPI.Dto
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
