using API.Repositories.Interface;
using EmployeeApp.Context;
using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class AbsensiRepository : IAbsensiRepository
    {
        MyContext myContext;

        public AbsensiRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.Absensi.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Absensi> Get()
        {
            var data = myContext.Absensi.Include(x => x.Karyawan).ThenInclude(x => x.Divisi).ToList();
            return data;
        }

        public Absensi Get(int id)
        {
            var data = myContext.Absensi.Include(x => x.Karyawan).ThenInclude(x => x.Divisi).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public int Post(Absensi absensi)
        {
            myContext.Absensi.Add(absensi);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Absensi absensi)
        {
            var data = Get(absensi.Id);
            if (data == null)
                return -1;
            data.Karyawan_Id = absensi.Karyawan_Id;
            data.Tanggal_Hadir = absensi.Tanggal_Hadir;
            myContext.Absensi.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
