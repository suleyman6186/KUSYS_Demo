using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        StudentManager sm = new StudentManager(new EfStudentRepository());
        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Index(UserSignUpViewModel p)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = p.Mail,
                    UserName = p.UserName,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                };

                //Record saving AspNetUsers table
                var _Create = await _userManager.CreateAsync(user, p.Password);

                if (_Create.Succeeded)
                {
                    var _AddToRole = await _userManager.AddToRoleAsync(user, "User");
 
                    if(_AddToRole.Succeeded)
                    {
                        //Saved user adding to student table. We have only one admin and more user
                        //Each user registered in the system is also registered as a student.
                        //Because there is only 1 Admin user. It has already been seeded.
                        Student student = new Student
                        {
                            StudentId = p.UserName,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            BirthDate = p.BirthDate
                        };
                        sm.StudentAdd(student);
                        return RedirectToAction("Index", "Login");
                    }  
                }
                else
                {
                    foreach(var item in _Create.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }
    }
}
