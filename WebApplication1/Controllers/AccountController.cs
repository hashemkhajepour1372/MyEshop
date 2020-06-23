using DataLayer;
using DataLayer.AccountViewModel;
using MyEshop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MyEshop_DBEntities db = new MyEshop_DBEntities();
        private IUserRepository userRepository;
        public AccountController()
        {
            userRepository = new UserRepository(db);
        }
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!userRepository.IsValidEmail(register.Email))
                {
                    Users user = new Users()
                    {
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RegisterDate = DateTime.Now,
                        RoleID = 1,
                        UserName = register.UserName

                    };
                    string Body = PartialToStringClass.RenderPartialView("ManageEmails","ActivationEmail",user);
                    SendEmail.Send(user.Email, "ایمیل فعال سازی", Body);
                    userRepository.InsertUser(user);
                    userRepository.save();
               
                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است.");
                }
            }
            return View(register);
        }
       
        public ActionResult ActiveUser(string id)
        {
            var user = userRepository.GetUserByActivecode(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            userRepository.save();
            ViewBag.UserName = user.UserName;
            return View();
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login , string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = userRepository.GetUserByEmailandpassword(login.Email, hashpassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMi);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد");
                }
            }
            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}