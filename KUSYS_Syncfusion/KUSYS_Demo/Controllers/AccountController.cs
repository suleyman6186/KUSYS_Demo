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
    public class AccountController : Controller
    {
        StudentManager sm = new StudentManager(new EfStudentRepository());

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordViewModel p)
        {
            if (ModelState.IsValid)
            {
                //Mail sender codes will be here
                //Send reset password e-mail to p.Mail

                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public async Task<IActionResult> Profile()
        {
            //Getting user info from AspNetUsers table
            var UserInfo = await _userManager.FindByNameAsync(User.Identity?.Name);
            //Getting role info from AspNetUserRoles table
            var RoleInfo = await _userManager.GetRolesAsync(UserInfo);
            if (RoleInfo != null)
            {
                string Role = RoleInfo[0];
                if (Role == "User")
                {
                    var model = sm.GetStudentByStudentID(UserInfo.UserName).First();
                    model.Role = Role;
                    return View(model);
                }
                else
                {
                    Student s = new Student();
                    s.StudentId = UserInfo.UserName;
                    s.FirstName = UserInfo.FirstName;
                    s.LastName = UserInfo.LastName;
                    s.BirthDate = UserInfo.BirthDate;
                    s.Role = Role;
                    return View(s);
                }
            }

            return View();
        }

    }
}