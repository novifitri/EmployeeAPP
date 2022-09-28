using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
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
        public DbSet<Role> Role { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

    }
}
