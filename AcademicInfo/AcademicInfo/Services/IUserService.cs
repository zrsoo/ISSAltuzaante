
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;

namespace AcademicInfo.Services
{
    public interface IUserService
    {
        Task UpdateAsync(UpdateUserModel foundUser);
        Task<Response> UpdatePasswordAsync(UpdatePasswordModel user);

        public Task<JwtSecurityToken> GenerateJwt(AcademicUser user);

        public Task<List<AcademicUser>> GetAllStudents();
        
        public Task<List<Grade>> GetAllGrades();


        Task grantScholarships(List<GradeDTO> keptGrades);
    }
}