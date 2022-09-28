using EmployeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IAbsensiRepository
    {
        List<Absensi> Get();
        Absensi Get(int id);
        int Post(Absensi absensi);
        int Put(Absensi absensi);
        int Delete(int id);
    }
}
