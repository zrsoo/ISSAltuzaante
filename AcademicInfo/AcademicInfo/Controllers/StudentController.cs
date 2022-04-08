using System.IdentityModel.Tokens.Jwt;
using AcademicInfo.Models;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AcademicInfo.Services;


namespace AcademicInfo.Controllers
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Student> _studentManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        
        [ActivatorUtilitiesConstructor]
        public AuthenticateController(IUserService userService, IConfiguration configuration, UserManager<Student> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _studentManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }
        
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

            var user = await _studentManager.FindByNameAsync(model.Email);
            if (user != null && await _studentManager.CheckPasswordAsync(user, model.Password))
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
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = from state in ModelState.Values
                    from error in state.Errors
                    select error.ErrorMessage;

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Success = false, Message = "Error creating user!", Errors = errors.ToList()});

            }

            var userExists = await _studentManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false, Message = "Error creating user!",
                        Errors = new List<string> {"User already exists!"}
                    });

            Student student = new()
            {
                Email = model.Email!,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Password = model.Password!,
                UserName = model.Email,
                City = model.City,
                Year = model.Year.ToString()
            };

            var result = await _studentManager.CreateAsync(student, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false, Message = "Error creating user!",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    });

            // add User role to Student
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            await _studentManager.AddToRoleAsync(student, UserRoles.User);


            return Ok(new Response {Success = true, Message = "User created successfully!"});
        }
    }
}

