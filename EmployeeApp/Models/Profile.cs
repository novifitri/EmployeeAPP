using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Password harus 8 karakter", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Karyawan Karyawan { get; set; }
        [ForeignKey("Karyawan")]
        public int Karyawan_Id { get; set; }
    }
}
