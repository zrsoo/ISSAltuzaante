using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;

namespace AcademicInfo.Repository
{
    public interface IUserRepo
    {
        Task UpdateFirstNameAsync(UpdateUserModel foundUser);

        Task UpdateLastNameAsync(UpdateUserModel foundUser);

        Task UpdateDisciplineAsync(string email, int optionalId);

        Task<bool> UpdatePassword(UpdatePasswordModel foundUser);

        Task<AcademicUser> FindById(Guid id);

        Task<List<AcademicUser>> GetAllStudents();

        Task<List<AcademicUser>> GetAll();
        
        Task<List<Grade>> GetAllGrades();
        Task grantScholarships(List<GradeDTO> keptGrades);
        Task<bool> UpdateApproval(String email);
    }
}