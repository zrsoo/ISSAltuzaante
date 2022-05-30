using AcademicInfo.Config;
using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AcademicUser> _userManager;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _dbContext;

        public UserController(UserManager<AcademicUser> userManager, IUserService userService, ICurrentUserService currentUserService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _dbContext = dbContext;
        }

        //update a user's first name and last name if the recieved request is valid
        [HttpPatch]
        [Route("updateuser")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserModel user)
        {
            var currentUserEmail = _currentUserService.GetUserId();
            user.Email = currentUserEmail;

            if (currentUserEmail != null)
            {
                try
                {
                    await _userService.UpdateAsync(user);
                    return Ok(new Response { Success = true, Message = "User updated successfully!" });
                }
                catch (ArgumentException exc)
                {
                    return BadRequest(new Response
                    {
                        Success = false,
                        Message = "The user could not be updated due to bad arguments",
                        Errors = new List<String> { exc.Message }
                    });
                }
                catch (Exception exc)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Success = false,
                        Message = "The user could not be updated",
                        Errors = new List<String> { exc.Message }
                    });
                }
            }
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Error updating user!" });
        }

        //update a user's password after checking if it is a valid one (meets some criterias for security)
        [HttpPatch]
        [Route("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordModel user)
        {
            var currentUserEmail = _currentUserService.GetUserId();
            user.Email = currentUserEmail;

            if (currentUserEmail != null)
            {
                if (user.NewPassword == user.Password)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "The new password has to be different from the old one!" });
                }
                if (user.NewPassword != user.NewPasswordConfirm)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "The password confirmation does not match the new password!" });
                }

                var result = await _userService.UpdatePasswordAsync(user);
                if (result.Success == true)
                {
                    return Ok(new Response { Success = true, Message = "User password updated successfully!" });
                }
                else return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Error updating user!", Errors = new List<string> { "Email doesn't exist!" } }); ;
        }

        //get the current authenticated user
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

            return StatusCode(200, _currentUserService.GetUserId());
        }

        //get the emails of all the teachers
        [HttpGet]
        [Route("get-teacher-emails")]
        public async Task<List<UserEmailDTO>> GetTeachersEmails()
        {
            return await _userService.GetTeachersEmail();
        }
    }
}