using API.Repositories.Data;
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
        ProfileRepository profileRepository;

        public ProfileController(ProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = profileRepository.Get();
            return Ok(new { message = "data semua profile karyawan", statusCode = 200, data = data });
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = profileRepository.Get(id);
            if (data == null)
                return NotFound(new { message = "profile tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail profile", statudCode = 200, data = data });
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Profile profile)
        {
            var result = profileRepository.Put(profile);
            if (result == -1)
                return NotFound(new { message = "profile tidak ditemukan", statusCode = 200 });
            else if (result > 0)
                return Ok(new { message = "profile berhasil diubah", statusCode = 200 });
            return BadRequest(new { statusCode = 400, message = "profile gagal diubah" });

        }

        [HttpPost]
        public IActionResult Post(Profile profile)
        {
            if (ModelState.IsValid)
            {
                var result = profileRepository.Post(profile);
                if (result > 0)
                    return Ok(new { message = "profile berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "profile gagal ditambah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = profileRepository.Delete(id);
            if (result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            else if (result > 0)
            {
                return Ok(new { message = "profile berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "profile gagal dihapus" });
        }
    }
}
