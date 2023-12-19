using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Dto
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
