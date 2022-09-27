using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EmployeeApp.Models;
using EmployeeApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeApp.Controllers
{
    public class AbsensiController : Controller
    {
        MyContext myContext;

        public AbsensiController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var Absensi = myContext.Absensi.Include(x => x.Karyawan).ToList();
            return View(Absensi);
      
        }

        public IActionResult Details(int id)
        {
            var absensi = myContext.Absensi.Find(id);
            return View(absensi);
        }

        public List<Karyawan> GetKaryawans()
        {
            List<Karyawan> karyawans = myContext.Karyawan.ToList();
            return karyawans;

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.KaryawansSelectList = new SelectList(GetKaryawans(), "Id", "Nama");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Absensi absensi)
        {
            if (ModelState.IsValid)
            {
                myContext.Absensi.Add(absensi);
                var result = myContext.SaveChanges();
                if (result > 0) return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server bermasalah, Absensi gagal ditambah");
            return View();


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Karyawan> karyawans = GetKaryawans();
            ViewData["AllEmployee"] = karyawans;
            var absensi = myContext.Absensi.Find(id);
            return View(absensi);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Absensi absensi)
        {
            var absensiToUpdate = myContext.Absensi.FirstOrDefault(a => a.Id == absensi.Id);
            if (ModelState.IsValid)
            {
                absensiToUpdate.Karyawan_Id = absensi.Karyawan_Id;
                absensiToUpdate.Tanggal_Hadir = absensi.Tanggal_Hadir;
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            var karyawans = GetKaryawans();
            ViewData["AllKaryawan"] = karyawans;
            ModelState.AddModelError(string.Empty, "Absensi gagal diupdate");
            return View();
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var absensi = myContext.Absensi.Find(id);
            if (absensi == null)
            {
                return NotFound();
            }
            return View(absensi);
        }

        [HttpPost]
        public IActionResult Delete(Absensi a)
        {
            myContext.Absensi.Remove(a);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Absensi gagal dihapus");
            return View();
        }
    }
}
