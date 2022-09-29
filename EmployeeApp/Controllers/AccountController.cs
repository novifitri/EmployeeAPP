using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Controllers
{
    public class AccountController : Controller
    {
        HttpClient HttpClient;
        string address;
        public AccountController()
        {
            this.address = "https://localhost:44333/api/Account/";
            this.HttpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
  
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address, content).Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClient>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("Role", data.data.Role);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Email dan password salah");
            return View();
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterAPI(RegisterVM register)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address+"Register", content).Result;
            if (result.IsSuccessStatusCode)
            {
                return Redirect("Account/Login");
            }
            ModelState.AddModelError(string.Empty, "Registrasi gagal");
            return View();
        }

        [HttpGet]
        [Route("ForgetPassword")]
        public IActionResult ForgetPW(RegisterVM register)
        {        
            return View();
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPw(LoginVM login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address + "ForgetPassword", content).Result;
            if (result.IsSuccessStatusCode)
            {
                return Redirect("Account/Login");
            }
            ModelState.AddModelError(string.Empty, "Ganti Password gagal");
            return View();
        }
        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePW(LoginVM login)
        {
            return View();
        }
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePW(ChangePasswordVM changePassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePassword), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address + "ChangePassword", content).Result;
            if (result.IsSuccessStatusCode)
            {
                return Redirect("Account/Login");
            }
            ModelState.AddModelError(string.Empty, "Ganti Password gagal");
            return View();
        }
    }
}
