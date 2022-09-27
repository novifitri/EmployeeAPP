using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {
           
        }
        public DbSet<Divisi> Divisi { get; set; }
        public DbSet<Karyawan> Karyawan { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Absensi> Absensi { get; set; }
    }
}
