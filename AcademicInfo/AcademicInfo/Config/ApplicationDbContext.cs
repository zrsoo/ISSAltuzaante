using AcademicInfo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademicInfo.Config
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //configure the dbcontext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //for each model, we have to create a db set
        public DbSet<Student> Students { get; set; } // this will create a category table with the columns from the model
        // this is the code first approach (as opposed to database first)
    }
}