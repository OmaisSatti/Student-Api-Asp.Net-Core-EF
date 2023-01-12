using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Model
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string Contact { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email  { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        //public byte[] PasswordKey { get; set; }
    }
}
