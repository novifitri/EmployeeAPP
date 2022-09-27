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
    public class KaryawanController : ControllerBase
    {
        MyContext myContext;

        public KaryawanController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = myContext.Karyawan.Include(x => x.Divisi).ToList();
            return Ok(new { message = "data semua karyawan", statusCode = 200, data = data });
        }


        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var data = myContext.Karyawan.Include(x=> x.Divisi).FirstOrDefault(x => x.Id == id);
            if(data == null)
                return NotFound(new { message = "karyawan tidak ditemukan", statusCode = 200 });
            return Ok(new { message = "detail karyawan", statudCode = 200, data = data });
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Karyawan karyawan)
        {
            var data = myContext.Karyawan.Find(karyawan.Id);
            if (data == null)
                return NotFound(new { message = "karyawan tidak ditemukan", statusCode = 200 });
            data.Nama = karyawan.Nama;
            data.NIK = karyawan.NIK;
            data.Tanggal_Lahir = karyawan.Tanggal_Lahir;
            data.Jenis_Kelamin = karyawan.Jenis_Kelamin;
            data.Alamat = karyawan.Alamat;
            data.Nomor_Telp = karyawan.Nomor_Telp;
            data.Divisi_Id = karyawan.Divisi_Id;
            myContext.Karyawan.Update(data);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok(new { message = "karyawan berhasil diubah", statusCode = 200 });  
            return BadRequest(new { statusCode = 400, message = "karyawan gagal diubah" });
        }


        [HttpPost]
        public IActionResult Post(Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                myContext.Karyawan.Add(karyawan);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok(new { message = "karyawan berhasil ditambah", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "karyawan gagal ditambah" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = myContext.Karyawan.Find(id);
            if (data == null)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            myContext.Karyawan.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { message = "karyawan berhasil dihapus", statusCode = 200 });
            }
            return BadRequest(new { statusCode = 400, message = "karyawan gagal dihapus" });
        }
    }
}
