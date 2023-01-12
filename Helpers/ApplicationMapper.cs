using AutoMapper;
using StudentApi.Data;
using StudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
        }

    }
}
