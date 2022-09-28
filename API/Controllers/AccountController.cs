using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
            var result = accountRepository.Login(login);
            if (result == null)
                return NotFound(new { message = "gagal login user tidak ditemukan", statusCode = 404 });
            return Ok(new { message = "berhasil login", statusCode = 200, data = result });

        }
    }
}
