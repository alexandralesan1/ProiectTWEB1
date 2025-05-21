using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Enums;

namespace WebApplication4.Domain.Entities
{
     public class DBSessionTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          public string Email { get; set; }

          [Required]
          public string CookieString { get; set; }
          [Required]
          public UserRole Role { get; set; }

          [Required]
          public DateTime ExpireTime { get; set; } 
     }
}
