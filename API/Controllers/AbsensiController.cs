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
    public class AbsensiController : ControllerBase
    {
        MyContext myContext;

        public AbsensiController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = myContext.Absensi.Include(x => x.Karyawan).ThenInclude(x => x.Divisi).ToList();
            return Ok(new { message = "data semua absensi karyawan", statusCode = 200, data = data });
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = myContext.Absensi.Include(x => x.Karyawan).FirstOrDefault(x => x.Id == id);
            if (data == null)
                return NotFound(new { message = "absensi tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail absensi", statudCode = 200, data = data });
        }
        [HttpPut("{Id}")]
        public IActionResult Put(Absensi absensi)
        {
            var data = myContext.Absensi.Find(absensi.Id);
            if (data == null)
                return NotFound(new { message = "absensi tidak ditemukan", statusCode = 200 });
            data.Karyawan_Id = absensi.Karyawan_Id;
            data.Tanggal_Hadir = absensi.Tanggal_Hadir;
            myContext.Absensi.Update(data);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok(new { message = "absensi berhasil diubah", statusCode = 200 });
            return BadRequest(new { statusCode = 400, message = "absensi gagal diubah" });
        }

        [HttpPost]
        public IActionResult Post(Absensi absensi)
        {
            if (ModelState.IsValid)
            {
                myContext.Absensi.Add(absensi);      
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok(new { message = "absensi berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "absensi gagal ditambah" });


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = myContext.Absensi.Find(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            myContext.Absensi.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { message = "absensi berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "absensi gagal dihapus" });
        }
    }
}
