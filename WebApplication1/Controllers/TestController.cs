using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        [Authorize]
        public ActionResult test1()
        {
            return View();
        }
        [Authorize(Roles ="admin")]
        public ActionResult test2()
        {
            return View();
        }
    }
}