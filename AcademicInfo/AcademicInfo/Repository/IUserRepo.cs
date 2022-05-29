using AcademicInfo.Models;

namespace AcademicInfo.Repository
{
    public interface IUserRepo
    {
        Task UpdateFirstNameAsync(UpdateUserModel foundUser);

        Task UpdateLastNameAsync(UpdateUserModel foundUser);

        Task UpdateDisciplineAsync(string email, int optionalId);

        Task<bool> UpdatePassword(UpdatePasswordModel foundUser);

        Task<AcademicUser> FindById(Guid id);

        Task<bool> UpdateApproval(String email);
    }
}