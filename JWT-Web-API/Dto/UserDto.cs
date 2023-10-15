﻿namespace JWTWebAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
