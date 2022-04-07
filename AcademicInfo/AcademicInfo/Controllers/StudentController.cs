using AcademicInfo.Models;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AcademicInfo.Controllers
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Student> _studentManager;

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
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Password = model.Password!
            };

            var result = await _studentManager.CreateAsync(student, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false, Message = "Error creating user!",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    });


            return Ok(new Response {Success = true, Message = "User created successfully!"});
        }
    }
}

