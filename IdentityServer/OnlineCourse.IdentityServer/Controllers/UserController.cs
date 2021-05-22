using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.IdentityServer.Dtos;
using OnlineCourse.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCourse.Extensions;

namespace OnlineCourse.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUp(SignupDto dto)
        {

            var user = new ApplicationUser { Email = dto.Email, UserName = dto.UserName, City = dto.City };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return Ok(dto);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
