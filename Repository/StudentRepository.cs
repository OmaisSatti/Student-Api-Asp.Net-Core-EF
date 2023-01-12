using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly StudentContext _DB;
        private readonly IMapper _mapper;
        public StudentRepository(StudentContext Db,IMapper mapper)
        {
            _DB = Db;
            _mapper = mapper;
        }
        public async Task<List<StudentModel>> GetAllStudentsAsync()
        {
            //var lst = await _DB.Tbl_Students.Select(x => new StudentModel()
            //{
            //    ID = x.ID,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    EducationLevel = x.EducationLevel,
            //    Specialization = x.Specialization,
            //    UniversityName = x.UniversityName
            //}).ToListAsync();
            // return lst;
            var students = await _DB.Tbl_Students.ToListAsync();
            return _mapper.Map<List<StudentModel>>(students);
        }
        public async Task<StudentModel> GetStudentByIdAsync(int sid)
        {
            //var student = await _DB.Tbl_Students.Where(s=>s.ID==sid).Select(x => new StudentModel()
            //{
            //    ID = x.ID,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    EducationLevel = x.EducationLevel,
            //    Specialization = x.Specialization,
            //    UniversityName = x.UniversityName
            //}).FirstOrDefaultAsync();
            //return student;
            var students = await _DB.Tbl_Students.FindAsync(sid);
            return _mapper.Map<StudentModel>(students);
        }
        public async Task<int> AddStudentAsync(StudentModel studentModel) 
        {
            //var student = new Student()
            //{
            //    FirstName = studentModel.FirstName,
            //    LastName = studentModel.LastName,
            //    EducationLevel = studentModel.EducationLevel,
            //    Specialization = studentModel.Specialization,
            //    UniversityName = studentModel.UniversityName
            //};
            var std=_mapper.Map<Student>(studentModel);
            _DB.Tbl_Students.Add(std);
            return await _DB.SaveChangesAsync();

        }
        public async Task<int> UpdateStudentAsync(int sid,StudentModel studentModel) 
        {
            Student std = await _DB.Tbl_Students.FindAsync(sid);
            if (std != null)
            {
                //std.FirstName = studentModel.FirstName;
                //std.LastName = studentModel.LastName;
                //std.DOB = studentModel.DOB;
                //std.EducationLevel = studentModel.EducationLevel;
                //std.Specialization = studentModel.Specialization;
                //std.UniversityName = studentModel.UniversityName;
                _mapper.Map(studentModel, std);
            }
            else 
            {
                return -1;

            }
            return await _DB.SaveChangesAsync();
        }
        public async Task<int> DeleteStudentAsync(int sid)
        {
            var std = await _DB.Tbl_Students.FindAsync(sid);
            if (std!=null)
            {
                _DB.Tbl_Students.Remove(std);
                return await _DB.SaveChangesAsync();
            }
            else 
            {
                return -1;

            }
        }
    }
}
