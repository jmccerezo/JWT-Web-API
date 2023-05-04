using System.ComponentModel.DataAnnotations;

namespace JwtWebApi.Dto
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
