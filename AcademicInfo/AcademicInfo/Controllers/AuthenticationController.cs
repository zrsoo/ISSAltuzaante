﻿using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AcademicUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        [ActivatorUtilitiesConstructor]
        public AuthenticateController(IUserService userService, IConfiguration configuration, UserManager<AcademicUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        //in order to login, check the username, password, if the requested model is fine, and then send a security token if the login is successful
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage;

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Error logging in!", Errors = errors.ToList() });
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _userService.GenerateJwt(user);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        //register as a new user with the role of student, aafter checking if it already exists or the requested model is errorless
        [HttpPost]
        [Route("register-student")]
        public async Task<IActionResult> Register([FromBody] StudentRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage;

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Success = false, Message = "Error creating student!", Errors = errors.ToList() });
            }

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false,
                        Message = "Error creating student!",
                        Errors = new List<string> { "Student already exists!" }
                    });

            AcademicUser student = new()
            {
                Email = model.Email!,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Password = model.Password!,
                UserName = model.Email,
                City = model.City,
                Year = model.Year.ToString(),
                SpecializationId = model.SpecializationId,
                FacultyId = model.SpecializationId
            };

            var result = await _userManager.CreateAsync(student, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false,
                        Message = "Error creating user!",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    });

            // add User role to Student
            if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

            await _userManager.AddToRoleAsync(student, UserRoles.Student);

            return Ok(new Response { Success = true, Message = "User created successfully!" });
        }

        //register as a new user with the role of teacher, aafter checking if it already exists or the requested model is errorless
        [HttpPost]
        [Route("register-teacher")]
        public async Task<IActionResult> Register([FromBody] TeacherRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage;

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Success = false, Message = "Error creating teacher!", Errors = errors.ToList() });
            }

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false,
                        Message = "Error creating teacher!",
                        Errors = new List<string> { "Teacher already exists!" }
                    });

            AcademicUser teacher = new()
            {
                Email = model.Email!,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Password = model.Password!,
                UserName = model.Email,
                Degree = model.Degree,
                IsChiefOfDepartment = model.IsChiefOfDepartment,
                FacultyId = model.FacultyId
            };

            var result = await _userManager.CreateAsync(teacher, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false,
                        Message = "Error creating user!",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    });

            // add User role to Teacher
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));

            await _userManager.AddToRoleAsync(teacher, UserRoles.Teacher);

            return Ok(new Response { Success = true, Message = "User created successfully!" });
        }

        //return a dto of the current user if there is one(search it in db by its email)
        [HttpGet]
        [Route("get-authenticated-user")]
        public async Task<ActionResult> getAuthenticatedUserByTokenAsync()
        {
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            AcademicUser user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return null;
            UserDTO userDTO = new UserDTO(user);

            return StatusCode(200, userDTO);
        }
    }
}