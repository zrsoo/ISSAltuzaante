﻿namespace AcademicInfo.Models.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }= null;
        public string FirstName { get; set; }= null;
        public string LastName { get; set; }= null;
        public UserDTO(AcademicUser user)
        {
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName; 
        }

        public UserDTO()
        {
        }
    }
}