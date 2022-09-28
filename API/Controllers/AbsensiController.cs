using API.Models;
using API.Repositories.Data;
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
    public class AbsensiController : ControllerBase
    {
        AbsensiRepository absensiRepository;

        public AbsensiController(AbsensiRepository absensiRepository)
        {
            this.absensiRepository = absensiRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = absensiRepository.Get();
            return Ok(new { message = "data semua absensi karyawan", statusCode = 200, data = data });
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = absensiRepository.Get(id);
            if (data == null)
                return NotFound(new { message = "absensi tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail absensi", statudCode = 200, data = data });
        }
        [HttpPut("{Id}")]
        public IActionResult Put(Absensi absensi)
        {
            var result = absensiRepository.Put(absensi);
            if (result == -1)
                return NotFound(new { message = "absensi tidak ditemukan", statusCode = 200 });
            else if (result > 0)
                return Ok(new { message = "absensi berhasil diubah", statusCode = 200 });
            return BadRequest(new { statusCode = 400, message = "absensi gagal diubah" });
        }

        [HttpPost]
        public IActionResult Post(Absensi absensi)
        {
            if (ModelState.IsValid)
            {
                var result = absensiRepository.Post(absensi);
                if (result > 0)
                    return Ok(new { message = "absensi berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "absensi gagal ditambah" });


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = absensiRepository.Delete(id);
            if (result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            else if (result > 0)
            {
                return Ok(new { message = "absensi berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "absensi gagal dihapus" });
        }
    }
}
