using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Absensi
    {
        [Key]
        public int Id { get; set; }
        public Karyawan Karyawan { get; set; }

        [ForeignKey("Karyawan")]
        public int Karyawan_Id { get; set; }
        [Required]
        public DateTime Tanggal_Hadir { get; set; }

    }
}
