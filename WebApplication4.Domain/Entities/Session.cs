using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Domain.Entities
{
     public class DBSessionTable
     {
          [Key]
          public int Id { get; set; }

          [Required]
          public string Username { get; set; }

          [Required]
          public string CookieString { get; set; }

          [Required]
          public DateTime ExpireTime { get; set; } 
     }
}
