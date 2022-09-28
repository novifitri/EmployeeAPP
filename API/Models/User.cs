using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        [Key]
        public Employee Employee { get; set; }
        [ForeignKey("Employee")]

        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
