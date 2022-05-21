﻿using AcademicInfo.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AcademicInfo.Services
{
    public interface IUserService
    {
        Task UpdateAsync(UpdateUserModel foundUser);

        Task<Response> UpdatePasswordAsync(UpdatePasswordModel user);

        public Task<JwtSecurityToken> GenerateJwt(AcademicUser user);

        public Task UpdateDisciplineAsync(String email, int optionalId);
    }
}