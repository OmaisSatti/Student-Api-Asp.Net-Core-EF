using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Model
{
    public class StudentModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EducationLevel { get; set; }
        public string Specialization { get; set; }
        public string UniversityName { get; set; }
    }
}
