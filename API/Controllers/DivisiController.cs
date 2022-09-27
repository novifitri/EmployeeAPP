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
        MyContext myContext;

        public DivisiController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = myContext.Divisi.ToList();
            return Ok(new { message = "data semua divisi", statudCode = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = myContext.Divisi.Find(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            return Ok(new { message = "detail divisi", statudCode = 200, data = data });
        }

        [HttpPost]
        public IActionResult Post(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                myContext.Divisi.Add(divisi);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok(new { message = "divisi berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal ditambah" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(Divisi divisi)
        {
            var divisiToUpdate = myContext.Divisi.Find(divisi.Id);
            if(divisiToUpdate == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            divisiToUpdate.Nama = divisi.Nama;
            myContext.Divisi.Update(divisiToUpdate);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { message = "divisi berhasil diubah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal diubah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = myContext.Divisi.Find(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            myContext.Divisi.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { message = "divisi berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "divisi gagal dihapus" });
        }
    }
}
