namespace AcademicInfo.Models.DTOs
{
    public class UserEmailDTO
    {
        public UserEmailDTO(string email)
        {
            Email = email;
        }

        public string Email { get; set; } = null;
    }
}
