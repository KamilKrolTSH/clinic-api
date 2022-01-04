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
    public class UserDiagnoseController : ControllerBase
    {
        private readonly MainContext _context;

        public UserDiagnoseController(MainContext context)
        {

            _context = context;
        }

        public static float GetScore(int x1, int x2, int x3, int x4, int x5, int x6, float x7, float x8)
        {
            float distance =
                x1 * x1 +
                x2 * x2 +
                x3 * x3 +
                x4 * x4 +
                x5 * x5 +
                x6 * x6 +
                x7 * x7 +
                x8 * x8;

            float score = MathF.Sqrt(distance);
            return score;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDiagnose>>> GetUserData()
        {
            string userName = HttpContext.User.Identity.Name;

            var userDiagnose = await _context.UserDiagnoses.Where((b) => b.UserName == userName).FirstOrDefaultAsync();

            if(userDiagnose == null){
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userDiagnose });
        }


        [HttpGet("simulate/{userName}")]
        public async Task<ActionResult<UserDiagnose>> SimulateUserDiagnose(string userName)
        {
            List<Diabetes> diabetesL = new List<Diabetes>(); 

            var userData = await _context.UserDatas.Where((b) => b.UserName == userName).FirstOrDefaultAsync();

            if(userData == null){
                return BadRequest();
            }

            var file = System.IO.File.ReadAllLines("Assets/diabetes.csv").Skip(1);

            foreach(string s in file) {
                var line = s.Split(",").ToList();

                Diabetes diabetes = new Diabetes();
                diabetes.Pregnancies = int.Parse(line[0]);
                diabetes.Glucose = int.Parse(line[1]);
                diabetes.BloodPressure = int.Parse(line[2]);
                diabetes.SkinThickness = int.Parse(line[3]);
                diabetes.Insulin = int.Parse(line[4]);
                diabetes.Bmi = float.Parse(line[5].Replace(".", ","));
                diabetes.DiabetesPedigreeFunction = float.Parse(line[6].Replace(".", ","));
                diabetes.Age = int.Parse(line[7]);
                diabetes.Outcome = int.Parse(line[8]);

                diabetesL.Add(diabetes);
            }

            int k = Convert.ToInt32(Math.Floor(Math.Sqrt(diabetesL.Count/2)));

            foreach(Diabetes d in diabetesL) {
                int x1 = d.Pregnancies - userData.Pregnancies;
                int x2 = d.Age - userData.Age;
                int x3 = d.BloodPressure - userData.BloodPressure;
                int x4 = d.SkinThickness - userData.SkinThickness;
                int x5 = d.Insulin - userData.Insulin;
                int x6 = d.Glucose - userData.Glucose;
                float x7 = d.Bmi - userData.Bmi;
                float x8 = d.DiabetesPedigreeFunction - userData.DiabetesPedigreeFunction;

                d.Score = GetScore(x1,x2,x3,x4,x5,x6,x7,x8);
            }

            diabetesL.Sort((x1, x2) => x1.Score.CompareTo(x2.Score));

            int healthy = 0;
            int sick = 0;

            for (int i = 0; i < k; i++) {
                if(diabetesL[i].Outcome.Equals(0)) {
                    healthy++;
                } else {
                    sick ++;
                }
            }

            UserDiagnose userDiagnose = new UserDiagnose();
            userDiagnose.Id = -1;
            userDiagnose.UserName = userName;
            userDiagnose.Value = sick > healthy;

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userDiagnose });
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserData>> GetUserDataByUserName(string userName)
        {
            var userDiagnose = await _context.UserDiagnoses.Where((b) => b.UserName == userName).FirstOrDefaultAsync();

            if(userDiagnose == null){
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userDiagnose });
        }

        [HttpPost]
        public async Task<ActionResult<UserDiagnose>> setUserDiagnose(UserDiagnoseDto userDiagnoseDto)
        {
            var user = await _context.Users.Where((b) => b.UserName == userDiagnoseDto.userName).FirstOrDefaultAsync();

            if(user == null){
                return NotFound();
            }

            var userDiagnose = await _context.UserDiagnoses.Where((b) => b.UserName == userDiagnoseDto.userName).FirstOrDefaultAsync();

            userDiagnose = userDiagnose != null ? userDiagnose : new UserDiagnose();

            userDiagnose.Value = userDiagnoseDto.Diagnose;
            userDiagnose.UserName = userDiagnoseDto.userName;
            _context.UserDiagnoses.Update(userDiagnose);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = userDiagnose });
        }

    }
}
