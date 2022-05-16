using AcademicInfo.Models;

namespace AcademicInfo.Repository
{
    public interface IUserRepo
    {
        Task UpdateFirstNameAsync(UpdateUserModel foundUser);
        Task UpdateLastNameAsync(UpdateUserModel foundUser);
        Task<bool> UpdatePassword(UpdatePasswordModel foundUser);
        Task<AcademicUser> FindById(Guid id);

        Task<List<AcademicUser>> GetAllStudents();

        Task<List<AcademicUser>> GetAll();
        
        Task<List<Grade>> GetAllGrades();
    }
}
