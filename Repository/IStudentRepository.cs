using Microsoft.AspNetCore.Mvc;
using StudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
    public interface IStudentRepository
    {
        Task<List<StudentModel>> GetAllStudentsAsync();
        Task<StudentModel> GetStudentByIdAsync(int sid);
        Task<int> AddStudentAsync(StudentModel studentModel);
        Task<int> UpdateStudentAsync(int sid, StudentModel studentModel);
        Task<int> DeleteStudentAsync(int sid);

    }
}
