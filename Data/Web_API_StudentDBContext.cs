using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_API_Student.BusinessLayer;

namespace Web_API_Student.Models
{
    public class Web_API_StudentDBContext : DbContext
    {
        public Web_API_StudentDBContext (DbContextOptions<Web_API_StudentDBContext> options)
            : base(options)
        {
        }

        public DbSet<Web_API_Student.BusinessLayer.Student> Student { get; set; }
    }
}
