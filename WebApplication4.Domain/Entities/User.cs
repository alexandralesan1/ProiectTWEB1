using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Enums;

namespace WebApplication4.Domain.Entities
{
    public class DBUserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        [Required]
        public string Password { get; set; }
          
        public bool IsBlocked { get; set; }

          [Required]
          public UserRole Role { get; set; }

     }


}
