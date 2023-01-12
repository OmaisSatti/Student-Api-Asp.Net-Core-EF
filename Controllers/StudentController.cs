using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Model;
using System.Threading.Tasks;
using StudentApi.Repository;
using Microsoft.AspNetCore.Authorization;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllStudents() 
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var students = await _studentRepository.GetStudentByIdAsync(id);
            return Ok(students);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            var student = await _studentRepository.AddStudentAsync(studentModel);
            if (student > 0)
            {
                return Ok("Record inserted successfully");
            }
            else 
            {
                return NotFound("Record not inserted");
            }
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromBody]StudentModel studentModel,[FromRoute] int id)
        {
            var result=await _studentRepository.UpdateStudentAsync(id,studentModel);
            if (result>0)
            {
                return Ok("Record updated successfully");
            }
            else 
            {
                return NotFound("Record not found");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var result=await _studentRepository.DeleteStudentAsync(id);
            if (result > 0)
            {
                return Ok("Record deleted successfully");
            }
            else
            {
                return Ok("Record not found");

            }
          
        }

    }
}
