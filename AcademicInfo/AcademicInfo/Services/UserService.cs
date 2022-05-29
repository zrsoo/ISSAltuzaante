using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace AcademicInfo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly UserManager<AcademicUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public UserService(UserManager<AcademicUser> userManager, IConfiguration configuration, IUserRepo userRepo, ICurrentUserService currentUserService)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }

        public async Task UpdateAsync(UpdateUserModel user)
        {
            if (!String.IsNullOrEmpty(user.FirstName))
            {
                await _userRepo.UpdateFirstNameAsync(user);
            }
            if (!String.IsNullOrEmpty(user.LastName))
            {
                await _userRepo.UpdateLastNameAsync(user);
            }
            //if (!String.IsNullOrEmpty(user.Password))
            //{
            //    await _userRepo.UpdatePasswordAsync(user);
            //}
        }

        public async Task<Response> UpdatePasswordAsync(UpdatePasswordModel user)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSpecialCharacter = new Regex(@"[!@#$%^&*]+");

            var isValidated = hasNumber.IsMatch(user.NewPassword) && hasUpperChar.IsMatch(user.NewPassword) && hasMinimum8Chars.IsMatch(user.NewPassword) && hasSpecialCharacter.IsMatch(user.NewPassword);

            if (isValidated)
            {
                bool isOldPasswordCorrect = await _userRepo.UpdatePassword(user);
                if (isOldPasswordCorrect)
                {
                    return new Response(true, "Password was reset successfully.");
                }
                else
                {
                    return new Response(false, "Old password is incorrect.");
                }
            }
            return new Response(false, "New password should contain at least one number, capital letter and should be at least 8 characters long.");
        }

        public async Task<JwtSecurityToken> GenerateJwt(AcademicUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim("Email", user.Email),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task UpdateDisciplineAsync(String email, int optionalId)
        {
            await _userRepo.UpdateDisciplineAsync(email, optionalId);
        }

        public async Task<List<UserEmailDTO>> GetTeachersEmail()
        {
            return await _userManager.Users
                .Select(user => new UserEmailDTO(user.Email))
                .ToListAsync();
        }

        public async Task<bool> ApproveOptional(String email)
        {
            return await _userRepo.UpdateApproval(email);
        }
    }
}