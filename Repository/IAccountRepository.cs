using Microsoft.AspNetCore.Identity;
using StudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignupAsync(SignupModel signupModel);
        Task<string> LoginAsync(SigninModel signinModel);
        Task<string> Login(string username, string password);
        Task<int> Signup(UserModel userModel);
        Task<int> Register(string name, string email,string contact, string password, string role);

    }
}
