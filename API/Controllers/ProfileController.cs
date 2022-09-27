using EmployeeApp.Context;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        MyContext myContext;

        public ProfileController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = myContext.Profile.Include(x => x.Karyawan).ToList();
            return Ok(new { message = "data semua profile karyawan", statusCode = 200, data = data });
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = myContext.Profile.Include(x => x.Karyawan).FirstOrDefault(x => x.Id == id);
            if (data == null)
                return NotFound(new { message = "profile tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail profile", statudCode = 200, data = data });
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Profile profile)
        {
            var data = myContext.Profile.Find(profile.Id);
            if (data == null)
                return NotFound(new { message = "profile tidak ditemukan", statusCode = 200 });
            data.Username = profile.Username;
            data.Email = profile.Email;
            data.Password = profile.Password;
            data.Karyawan_Id = profile.Karyawan_Id;
            myContext.Profile.Update(data);
            var result =  myContext.SaveChanges();
            if (result > 0)
                return Ok(new { message = "profile berhasil diubah", statusCode = 200 });
            return BadRequest(new { statusCode = 400, message = "profile gagal diubah" });

        }

        [HttpPost]
        public IActionResult Post(Profile profile)
        {
            if (ModelState.IsValid)
            {
                myContext.Profile.Add(profile);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok(new { message = "profile berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "profile gagal ditambah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = myContext.Profile.Find(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            myContext.Profile.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { message = "profile berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "profile gagal dihapus" });
        }
    }
}
