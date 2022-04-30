using AcademicInfo.Config;
using AcademicInfo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademicInfo.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<AcademicUser> _userManager;
        public ApplicationDbContext dbContext;

        public UserRepo(ApplicationDbContext dbContext, UserManager<AcademicUser> _uM)
        {
            this.dbContext = dbContext;
            this._userManager = _uM;
        }
        public Task<AcademicUser> FindById(Guid id)
        {
            var dbUser = dbContext.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
            return dbUser;
        }

        public async Task UpdateFirstNameAsync(UpdateUserModel foundUser)
        {
            AcademicUser dbUser = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == foundUser.Email);
            if (foundUser.FirstName != null)
            {
                dbUser.FirstName = foundUser.FirstName;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateLastNameAsync(UpdateUserModel foundUser)
        {
            AcademicUser dbUser = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == foundUser.Email);
            if (foundUser.LastName != null)
            {
                dbUser.LastName = foundUser.LastName;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePassword(UpdatePasswordModel user)
        {
            var userFound = await _userManager.FindByNameAsync(user.Email);

            if (await _userManager.CheckPasswordAsync(userFound, user.Password))
            {
                userFound.Password = user.NewPassword;
                await _userManager.ChangePasswordAsync(userFound, user.Password, user.NewPassword);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
