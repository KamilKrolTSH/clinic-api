using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly MainContext _context;

        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserController(MainContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
    
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetUsers() {
            List<ApplicationUser> users = new List<ApplicationUser>();
            
            var allUsers = await _context.Users.ToListAsync();

            foreach (var user in allUsers)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                if(userRoles.Contains("Admin") == false){
                    users.Add(user);
                }
            }            

            return StatusCode(StatusCodes.Status200OK, new Response { Content = users });
        }
    }
}
