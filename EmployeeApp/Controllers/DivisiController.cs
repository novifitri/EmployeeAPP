using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EmployeeApp.Models;
using EmployeeApp.Context;

namespace EmployeeApp.Controllers
{

    public class DivisiController : Controller
    {
        MyContext myContext;

        public DivisiController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            var data = myContext.Divisi.ToList();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var divisi = myContext.Divisi.Find(id);
            return View(divisi);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                myContext.Divisi.Add(divisi);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty, "Divisi gagal ditambah");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
        
            var divisi = myContext.Divisi.Find(id);
            if(divisi == null)
            {
                return NotFound();
            }
            return View(divisi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Divisi divisi)
        {
            var divisiToUpdate = myContext.Divisi.FirstOrDefault(d => d.Id == divisi.Id);
            divisiToUpdate.Nama = divisi.Nama;
            var result = myContext.SaveChanges();
            if(result > 0)
            {
                return RedirectToAction("Index");
            } 
            ModelState.AddModelError(string.Empty, "Divisi gagal diupdate");
            return View();

        }
      
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var divisi = myContext.Divisi.Find(id);
            if (divisi == null)
            {
                return NotFound();
            }
            return View(divisi);
        }

        [HttpPost]
        public IActionResult Delete(Divisi divisi)
        {
            myContext.Divisi.Remove(divisi);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Divisi gagal dihapus");
            return View();
        }
    }
}
