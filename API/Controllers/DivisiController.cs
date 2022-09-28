using API.Repositories.Data;
using EmployeeApp.Context;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisiController : ControllerBase
    {
        DivisiRepository divisiRepository;

        public DivisiController(DivisiRepository divisiRepository)
        {
            this.divisiRepository = divisiRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = divisiRepository.Get();
            return Ok(new { message = "data semua divisi", statudCode = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = divisiRepository.Get(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            return Ok(new { message = "detail divisi", statudCode = 200, data = data });
        }

        [HttpPost]
        public IActionResult Post(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                var result = divisiRepository.Post(divisi);
                if (result > 0)
                    return Ok(new { message = "divisi berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal ditambah" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(Divisi divisi)
        {
            var result = divisiRepository.Put(divisi);
            if(result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            else if (result > 0)
            {
                return Ok(new { message = "divisi berhasil diubah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal diubah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = divisiRepository.Delete(id);
   
            if(result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            if (result > 0)
            {
                return Ok(new { message = "divisi berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal dihapus" });
        }
    }
}
