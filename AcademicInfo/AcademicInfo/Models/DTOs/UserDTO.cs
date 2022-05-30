namespace AcademicInfo.Models.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;

        public string IsStudent { get; set; } = null;
        public bool IsApproved { get; set; }
        public bool HasSigned { get; set; }

        public UserDTO(AcademicUser user)
        {
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            if (user.Degree != null && user.Degree != "Admin")
                this.IsStudent = "No";
            else if (user.Degree != null && user.Degree == "Admin")
                this.IsStudent = "Admin";
            else
                this.IsStudent = "Yes";

            this.IsApproved = user.IsAproved;
            this.HasSigned = user.isSigned;
        }

        public UserDTO()
        {
        }
    }
}