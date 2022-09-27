using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    public class KaryawanController : Controller
    {
        MyContext myContext;

        public KaryawanController(MyContext context)
        {
            this.myContext = context;
        }
        public IActionResult Index()
        {
             var data = myContext.Karyawan.Include(x => x.Divisi).ToList();
            return View(data);
        
        }

        public IActionResult Details(int id)
        {
            var karyawan = myContext.Karyawan.Find(id);
            return View(karyawan);
        }
        public List<Divisi> getAllDivisi()
        {
            var allDivision = myContext.Divisi.ToList();
            return allDivision;
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Divisi> allDivision = getAllDivisi();
            ViewData["AllDivision"] = allDivision;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Karyawan karyawan)
        {     
            if (ModelState.IsValid)
            {
                myContext.Karyawan.Add(karyawan);
                var result = myContext.SaveChanges();
                if (result > 0) return RedirectToAction("Index");
            }
            List<Divisi> allDivision = getAllDivisi();
            ViewData["AllDivision"] = allDivision;
            ModelState.AddModelError(string.Empty, "Karyawan gagal ditambah");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var karyawan = myContext.Karyawan.Find(id);
            if (karyawan == null) return NotFound();
            List<Divisi> allDivision = getAllDivisi();
            ViewData["AllDivision"] = allDivision;
            return View(karyawan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Karyawan karyawan)
        {
  
            var karyawanToUpdate = myContext.Karyawan.FirstOrDefault(d => d.Id == karyawan.Id);
            karyawanToUpdate.Nama = karyawan.Nama;
            karyawanToUpdate.NIK = karyawan.NIK;
            karyawanToUpdate.Jenis_Kelamin = karyawan.Jenis_Kelamin;
            karyawanToUpdate.Tanggal_Lahir = karyawan.Tanggal_Lahir;
            karyawanToUpdate.Alamat = karyawan.Alamat;
            karyawanToUpdate.Nomor_Telp = karyawan.Nomor_Telp;
            karyawanToUpdate.Divisi_Id = karyawan.Divisi_Id;
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Karyawan gagal diupdate");
            List<Divisi> allDivision = getAllDivisi();
            ViewData["AllDivision"] = allDivision;
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var karyawan = myContext.Karyawan.Find(id);
            if (karyawan == null)
            {
                return NotFound();
            }
            return View(karyawan);
        }
        [HttpPost]
        public IActionResult Delete(Karyawan karyawan)
        {  
            myContext.Karyawan.Remove(karyawan);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Karyawan gagal dihapus");
            return View();
        }
    }
}
