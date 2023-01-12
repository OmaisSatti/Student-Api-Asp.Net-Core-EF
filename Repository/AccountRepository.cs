using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentApi.Data;
using StudentApi.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly StudentContext _DB;
        private readonly IMapper _mapper;
        public AccountRepository(StudentContext db, IMapper mapper, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _DB = db;
            _mapper = mapper;
        }
        public async Task<int> Register(string name,string email,string contact,string password,string role)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
            var exist = _DB.Tbl_User.FirstOrDefault(u => u.Email == email);
            if (exist != null)
            {
                return -1;
            }
            else
            {
                var user = new User();
                user.Name = name;
                user.PasswordHash = passwordHash;
                user.PasswordKey = passwordKey;
                user.Role = role;
                user.Email = email;
                user.Contact = contact;
                _DB.Tbl_User.Add(user);
                return await _DB.SaveChangesAsync();
            }

        }
        public async Task<int> Signup(UserModel userModel)
        {
            var exist = _DB.Tbl_User.FirstOrDefault(u => u.Email == userModel.Email);
            if (exist!=null) 
            {
                return -1;
            }
            else
            {
                var user = _mapper.Map<User>(userModel);
                _DB.Tbl_User.Add(user);
                return await _DB.SaveChangesAsync();
            }

        }
        public async Task<string> Login(string email, string passwordText)
        {
            var user = await _DB.Tbl_User.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email));
            if (user == null)
            {
                return "User not found";
            }
            if (MatchPasswordHash(passwordText, user.PasswordHash, user.PasswordKey))
            {
                var auth = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role),
            };
                var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: auth,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "Something wen't wrong";
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hamc=new HMACSHA512(passwordKey))
            {
                var passwordHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
                for (int i = 0; i <passwordHash.Length; i++)
                {
                    if (passwordHash[i]!=password[i]) 
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        //---------------Second Method---------------

        public async Task<string> LoginAsync(SigninModel signinModel)
           {

            var results = await _signInManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, false, false);
            if (results.Succeeded)
            {
                return null;

            }
            var auth = new List<Claim>
            {
                new Claim(ClaimTypes.Name,signinModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires : DateTime.Now.AddDays(1),
                claims:auth,
                signingCredentials:new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }

        public async Task<IdentityResult> SignupAsync(SignupModel signupModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = (signupModel.FirstName + signupModel.LastName).ToLower()
            };
            return await _userManager.CreateAsync(user, signupModel.Password);

        }

       
    }
}
 