using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.AccountViewModel
{
    public class RegisterViewModel
    {
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید.")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Compare("Password" , ErrorMessage ="کلمه عبور مغایرت دارد.")]
        [DataType(DataType.Password)]
        public string Repasword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage =("ایمیل وارد شده معتبر نمی باشد."))]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="مرا به خاطر بسپار")]
        public bool RememberMi { get; set; }
    }
}
