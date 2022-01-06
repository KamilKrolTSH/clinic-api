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
    public class UserDataController : ControllerBase
    {
        private readonly MainContext _context;

        public UserDataController(MainContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserData()
        {
            string userName = HttpContext.User.Identity.Name;

            var userData = await _context.UserDatas.Where((b) => b.UserName == userName).FirstOrDefaultAsync();

            if(userData == null){
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userData });
        }

                
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserDatas()
        {

            var userData = await _context.UserDatas.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userData });
        }
        
        [HttpGet("{userName}")]
        public async Task<ActionResult<UserData>> GetUserDataByUserName(string userName)
        {
            var userData = await _context.UserDatas.Where((b) => b.UserName == userName).FirstOrDefaultAsync();

            if(userData == null){
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userData });
        }

        [HttpPost]
        public async Task<ActionResult> setUserData(UserDataDto userDataDto)
        {

            string userName = HttpContext.User.Identity.Name;

            UserData userData = new UserData();

            userData.UserName = userName;
            userData.Pregnancies = userDataDto.Pregnancies;
            userData.Glucose = userDataDto.Glucose;
            userData.BloodPressure = userDataDto.BloodPressure;
            userData.DiabetesPedigreeFunction = userDataDto.DiabetesPedigreeFunction;
            userData.Insulin = userDataDto.Insulin;
            userData.Bmi = userDataDto.Bmi;
            userData.Age = userDataDto.Age;
            userData.SkinThickness = userDataDto.SkinThickness;

            _context.UserDatas.Update(userData);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { });
        }

    }
}
