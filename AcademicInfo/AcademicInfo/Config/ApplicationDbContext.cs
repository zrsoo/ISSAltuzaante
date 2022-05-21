using AcademicInfo.Models;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademicInfo.Config
{
    public partial class ApplicationDbContext : IdentityDbContext<AcademicUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicUser> Students { get; set; } = null!;
        public virtual DbSet<LoginModel> Logins { get; set; } = null!;
        public virtual DbSet<StudentRegisterModel> StudentRegisters { get; set; } = null!;
        public virtual DbSet<TeacherRegisterModel> TeacherRegisters { get; set; } = null!;
        public virtual DbSet<UpdatePasswordModel> PasswordsUpdates { get; set; } = null!;
        public virtual DbSet<UpdateUserModel> UsersUpdates { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Discipline> Disciplines { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:altuzaante-server.database.windows.net,1433;Initial Catalog=altuzaante7;Persist Security Info=False;User ID=altuzaanteadmin;Password=Admin!!!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}