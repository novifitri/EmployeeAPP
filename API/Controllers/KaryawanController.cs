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
    public class KaryawanController : ControllerBase
    {
        KaryawanRepository karyawanRepository;

        public KaryawanController(KaryawanRepository karyawanRepository)
        {
            this.karyawanRepository = karyawanRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = karyawanRepository.Get();
            return Ok(new { message = "data semua karyawan", statusCode = 200, data = data });
        }


        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = karyawanRepository.Get(id);
            if(data == null)
                return NotFound(new { message = "karyawan tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail karyawan", statudCode = 200, data = data });
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Karyawan karyawan)
        {
            var result = karyawanRepository.Put(karyawan);
            if (result == -1)
                return NotFound(new { message = "karyawan tidak ditemukan", statusCode = 200 });
            else if (result > 0)
                return Ok(new { message = "karyawan berhasil diubah", statusCode = 200 });  
            return BadRequest(new { statusCode = 400, message = "karyawan gagal diubah" });
        }


        [HttpPost]
        public IActionResult Post(Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                var result = karyawanRepository.Post(karyawan);
                if (result > 0)
                    return Ok(new { message = "karyawan berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "karyawan gagal ditambah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = karyawanRepository.Delete(id);
            if (result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            if (result > 0)
            {
                return Ok(new { message = "karyawan berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "karyawan gagal dihapus" });
        }
    }
}
