using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentAuth.Models;

namespace IdentAuth.Data
{
    public static class Seeder
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student { FirstName = "Rahul",   LastName = "Mishra",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Sanju", LastName = "Samson",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Rithika",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gyanchand",    LastName = "Chaudhury",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Bhaskar",      LastName = "Reddy",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Sourov",    LastName = "Mukherjee",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nanda",     LastName = "Kumar",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            new Course{CourseID=1045,Title="Calculus",Credits=4},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            new Course{CourseID=2021,Title="Composition",Credits=3},
            new Course{CourseID=2042,Title="Literature",Credits=4}
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
        public static async Task SeedRolesAsync(UserManager<CustomIdent> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.DefaultRoles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.DefaultRoles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.DefaultRoles.Basic.ToString()));
        }

        public static async Task SeedDefaultAdmin(UserManager<CustomIdent> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin
            var defUser = new CustomIdent
            {
                UserName = "superadmin@arviconsulting.com",
                Email = "superadmin@arviconsulting.com",
                FirstName = "Super",
                LastName = "Admin",
                PhoneNumber = "9150348555",
                ProfilePicture = null,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defUser, "123P@ssw0rd!!.");
                    await userManager.AddToRoleAsync(defUser, Enums.DefaultRoles.SuperAdmin.ToString());
                }
            }
        }

        //public static byte[] GetImage()
        //{
        //    string path = _env.ContentRootPath;

        //    byte[] photo = File.ReadAllBytes("./images/login.png");
        //    return photo;
        //}
    }
}
