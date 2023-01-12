using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Model;
using StudentApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly StudentContext _db;
        public AccountController(StudentContext db,IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Signin([FromBody] UserModel userModel)
        {
            var results = await _accountRepository.Login(userModel.Email,userModel.Password);
            if (string.IsNullOrEmpty(results))
            {
                return Unauthorized();


            }
            return Ok(results);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserModel userModel)
        {
            var count = await _accountRepository.Signup(userModel);
            if (count > 0)
            {
                return Ok("User registered successfully");
            }
            else if (count==-1)
            {
                return BadRequest("user with this email already exist");
            }
            else
            {
                return Unauthorized("user registration faild");
            }

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            var count = await _accountRepository.Register(userModel.Name, userModel.Email,userModel.Contact,userModel.Password, userModel.Role);
            if (count > 0)
            {
                return Ok("User registered successfully");
            }
            else if (count == -1)
            {
                return BadRequest("user with this email already exist");
            }
            else
            {
                return Unauthorized("user registration faild");
            }

        }

        //-----------second method call-----------------
        [HttpPost("signupuser")]
        public async Task<IActionResult> SignupUser([FromBody] SignupModel signupModel)
        {
            var results = await _accountRepository.SignupAsync(signupModel);
            if (results.Succeeded)
            {
                return Ok("user register successfully");

            }
            return Unauthorized();

        }
        [HttpPost("loginuser")]
        public async Task<IActionResult> LoginUser([FromBody] SigninModel signinModel)
        {
            var results = await _accountRepository.LoginAsync(signinModel);
            if (string.IsNullOrEmpty(results))
            {
                return Unauthorized();
                

            }
            return Ok(results);
        }
    }
}
