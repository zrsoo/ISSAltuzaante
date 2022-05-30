using AcademicInfo.Config;
using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using Microsoft.AspNetCore.Components.Forms;
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
        
        public async Task<List<AcademicUser>> GetAllStudents()
        {
            return dbContext.Students.ToList();
        }
        
        public async Task<List<AcademicUser>> GetAll()
        {
            return await dbContext.Set<AcademicUser>().ToListAsync();
        }
        
        public async Task<List<Grade>> GetAllGrades()
        {
            return await dbContext.Set<Grade>().ToListAsync();
        }

        public async Task grantScholarships(List<GradeDTO> keptGrades)
        {
            //(from p in dbContext.Students
            //      where keptGrades.FindIndex(f => f.ID == p.Email) >= 0 select p).ToList()
            //.ForEach(x => x.PhoneNumber = "scholarship");

            var student_list = dbContext.Students.ToList().Where(s => keptGrades.FindIndex(f => f.ID == s.Email) >= 0).ToList();
            List<AcademicUser> results = (from p in student_list
                                          select p).ToList();

            foreach (AcademicUser p in results)
            {
                p.PhoneNumber = "scholarship";
            }

            dbContext.SaveChanges();
        }
        public async Task UpdateDisciplineAsync(string email, int optionalId)
        {
            AcademicUser dbUser = await dbContext.Users.FirstAsync(user => user.Email == email);
            dbUser.DisciplineId = optionalId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateApproval(String email)
        {
            AcademicUser dbUser = await dbContext.Users.FirstAsync(user => user.Email == email);
            dbUser.IsAproved = true;
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}