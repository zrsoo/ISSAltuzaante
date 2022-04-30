using AcademicInfo.Models;

namespace AcademicInfo.Repository
{
    public interface IUserRepo
    {
        Task UpdateFirstNameAsync(UpdateUserModel foundUser);
        Task UpdateLastNameAsync(UpdateUserModel foundUser);
        Task<bool> UpdatePassword(UpdatePasswordModel foundUser);
        Task<AcademicUser> FindById(Guid id);
    }
}
