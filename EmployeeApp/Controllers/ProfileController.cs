using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    public class ProfileController : Controller
    {
        MyContext myContext;
        public ProfileController(MyContext context)
        {
            this.myContext = context;
        }
        public IActionResult Index()
        {
            var profiles = myContext.Profile.Include(x => x.Karyawan).ToList();
            return View(profiles);
        }

     
        public List<Karyawan> GetKaryawans()
        {
            List<Karyawan> karyawans = myContext.Karyawan.ToList();
            return karyawans;    

        }
        public IActionResult Details(int id)
        {
            var profile = myContext.Profile.Find(id);
            return View(profile);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Karyawan> karyawans = GetKaryawans();
            ViewData["AllKaryawan"] = karyawans;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                myContext.Profile.Add(profile);
                var result = myContext.SaveChanges();
                if (result > 0) return RedirectToAction("Index");
            }
            var karyawans = GetKaryawans();
            ViewData["AllKaryawan"] = karyawans;
            ModelState.AddModelError(string.Empty, "profile gagal ditambah");
            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var profile = myContext.Profile.Find(id);
            if (profile == null) return NotFound();
            var karyawans = GetKaryawans();
            ViewData["AllKaryawan"] = karyawans;
            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Profile profile)
        {
            var profileToUpdate = myContext.Profile.FirstOrDefault(d => d.Id == profile.Id);
            profileToUpdate.Username = profile.Username;
            profileToUpdate.Email = profile.Email;
            profileToUpdate.Password = profile.Password;
         
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Profile gagal diupdate");
            var karyawans = GetKaryawans();
            ViewData["AllKaryawan"] = karyawans;

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var profile = myContext.Profile.Find(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }
        [HttpPost]
        public IActionResult Delete(Profile profile)
        {
            myContext.Profile.Remove(profile);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Profile gagal dihapus");
            return View();
        }
    }
}
