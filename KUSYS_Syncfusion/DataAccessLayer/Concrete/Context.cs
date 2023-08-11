using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
    //IdentityDbContext inherits from DbContext class
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LENOVO; database=KUSYS_Demo; integrated security=true;");
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Matching> Matchings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedCourses(builder);
            SeedAdminInfo(builder);
            SeedRoles(builder);
            SeedUserRole(builder);
        }

        private static void SeedCourses(ModelBuilder builder)
        {
            builder.Entity<Course>().HasData
            (
                new Course() { Id = 1, CourseId = "CAL183", CourseName = "Mathematics I" },
                new Course() { Id = 2, CourseId = "CHE183", CourseName = "General Chemistry" },
                new Course() { Id = 3, CourseId = "CME111", CourseName = "Programming Languages I" },
                new Course() { Id = 4, CourseId = "CME113", CourseName = "Introduction to Computer Engineering" },
                new Course() { Id = 5, CourseId = "FOL181", CourseName = "Foreign Language I" },
                new Course() { Id = 6, CourseId = "HST181", CourseName = "Atatürk's Principles and History of Revolutions I" },
                new Course() { Id = 7, CourseId = "PHY183", CourseName = "General Physics I" },
                new Course() { Id = 8, CourseId = "CAL186", CourseName = "Mathematics II" },
                new Course() { Id = 9, CourseId = "CAL188", CourseName = "Linear Algebra" },
                new Course() { Id = 10, CourseId = "CME112", CourseName = "Programming Languages II" },
                new Course() { Id = 11, CourseId = "CME114", CourseName = "Probability and Statistics" },
                new Course() { Id = 12, CourseId = "FOL182", CourseName = "Foreign Language II" },
                new Course() { Id = 13, CourseId = "HST182", CourseName = "Atatürk's Principles and History of Revolutions II" },
                new Course() { Id = 14, CourseId = "PHY186", CourseName = "General Physics II" },
                new Course() { Id = 15, CourseId = "TRK182", CourseName = "Turkish Language II" },
                new Course() { Id = 16, CourseId = "CAL283", CourseName = "Differantial Equations" },
                new Course() { Id = 17, CourseId = "CME221", CourseName = "Logic Circuits" },
                new Course() { Id = 18, CourseId = "CME223", CourseName = "Circuit Analysis" },
                new Course() { Id = 19, CourseId = "CME225", CourseName = "Object Oriented Programming" },
                new Course() { Id = 20, CourseId = "CME227", CourseName = "Data Structures" },
                new Course() { Id = 21, CourseId = "FOL281", CourseName = "Technical Foreign Language I" },
                new Course() { Id = 22, CourseId = "CME224", CourseName = "Electronics" },
                new Course() { Id = 23, CourseId = "CME228", CourseName = "Internet Based Programming" },
                new Course() { Id = 24, CourseId = "CME222", CourseName = "Algorithms" },
                new Course() { Id = 25, CourseId = "CME226", CourseName = "Database Management" },
                new Course() { Id = 26, CourseId = "CME323", CourseName = "Numerical Analysis" },
                new Course() { Id = 27, CourseId = "CME325", CourseName = "Data Communıcation Systems" },
                new Course() { Id = 28, CourseId = "CME327", CourseName = "Signals and Systems" },
                new Course() { Id = 29, CourseId = "CME399", CourseName = "Industrial Practice I" },
                new Course() { Id = 30, CourseId = "FOL381", CourseName = "Reading and Speaking at Foreign Language" },
                new Course() { Id = 31, CourseId = "SOC381", CourseName = "Values Education" },
                new Course() { Id = 32, CourseId = "CME322", CourseName = "Automata Theory" },
                new Course() { Id = 33, CourseId = "CME324", CourseName = "Operating Systems" },
                new Course() { Id = 34, CourseId = "CME326", CourseName = "Computer Network" },
                new Course() { Id = 35, CourseId = "ESC302", CourseName = "Research and Presentation Skills" },
                new Course() { Id = 36, CourseId = "FOL282", CourseName = "Technical Foreign Language II" },
                new Course() { Id = 37, CourseId = "FOL382", CourseName = "Foreign Language for Business" },
                new Course() { Id = 38, CourseId = "CME321", CourseName = "Microprocessors" },
                new Course() { Id = 39, CourseId = "CME421", CourseName = "Senior Project I" },
                new Course() { Id = 40, CourseId = "CME425", CourseName = "Introduction to Data Mining" },
                new Course() { Id = 41, CourseId = "CME427", CourseName = "Programming of Mobile Devices" },
                new Course() { Id = 42, CourseId = "CME429", CourseName = "Introduction to Image Processing" },
                new Course() { Id = 43, CourseId = "CME435", CourseName = "Web Services" },
                new Course() { Id = 44, CourseId = "CME499", CourseName = "Industrial Practice II" },
                new Course() { Id = 45, CourseId = "ESC461", CourseName = "Introduction to Economy" },
                new Course() { Id = 46, CourseId = "CME320", CourseName = "Computer Architecture" },
                new Course() { Id = 47, CourseId = "CME422", CourseName = "Senior Project II" },
                new Course() { Id = 48, CourseId = "CME426", CourseName = "Software Engineering" },
                new Course() { Id = 49, CourseId = "CME440", CourseName = "Introduction to Bioinformatics" },
                new Course() { Id = 50, CourseId = "CME442", CourseName = "Special Topics in Computer Engineering II" },
                new Course() { Id = 51, CourseId = "ESC462", CourseName = "Ethics" },
                new Course() { Id = 52, CourseId = "CME448", CourseName = "Digital Signal Processing" },
                new Course() { Id = 53, CourseId = "TRK181", CourseName = "Turkish Language I" }
            );
        }

        private static void SeedAdminInfo(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasData
            (
                new AppUser()
                {
                    Id = 1,
                    FirstName = "SÜLEYMAN",
                    LastName = "İBRAHİMBAŞ",
                    UserName = "2010010217002",
                    BirthDate = new DateTime(1992, 06, 08, 0, 0, 0),
                    NormalizedUserName = "2010010217002",
                    Email = "suleymanibrahimbas@gmail.com",
                    NormalizedEmail = "SULEYMANIBRAHIMBAS@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEICR8I/p9jvLtA+XrvIYBsag0sQGPXYeYV3WJ0gbUBUzeN9UKKRT5e5O1tVBkX8K5w==",
                    SecurityStamp = "LVF4BKR5P3ZZZGEK5GENLFKCUB3ITSPI",
                    ConcurrencyStamp = "05e88500-0b29-40e8-8ad4-4964dbf54afe",
                    PhoneNumber = "5362361674",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData
            (
                new AppRole() { Id = 1, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new AppRole() { Id = 2, Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
            );
        }

        private static void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<AppUserRole>().HasData
            (
                new AppUserRole() { UserId = 1, RoleId = 1 }
            );
        }
    }
}