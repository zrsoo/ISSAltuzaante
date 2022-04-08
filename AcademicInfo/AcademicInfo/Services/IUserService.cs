
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AcademicInfo.Models;

namespace AcademicInfo.Services
{
    public interface IUserService
    {
        public Task<JwtSecurityToken> GenerateJwt(Student user);
        
    }
}