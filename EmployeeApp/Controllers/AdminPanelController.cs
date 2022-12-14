using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role.Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Unauthorized", "ErrorPage");
        }
    }
}
