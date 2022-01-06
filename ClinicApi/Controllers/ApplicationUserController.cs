using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicApi.Models;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly MainContext _context;

        public ApplicationUserController(MainContext context)
        {

            _context = context;
        }
    
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetUsers() {
            var users = await _context.Users.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Content = users });
        }
    }
}
