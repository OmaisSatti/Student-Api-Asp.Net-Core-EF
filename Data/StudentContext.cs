using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentApi.Data;

namespace StudentApi.Model
{
    public class StudentContext:IdentityDbContext<ApplicationUser>
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }
        public DbSet<Student> Tbl_Students { get; set; }
        public DbSet<User> Tbl_User { get; set; }
    }
}
