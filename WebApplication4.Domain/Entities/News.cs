using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Domain.Entities
{
     public class DBNewsTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [StringLength(120)]
          public string Title { get; set; }

          [Required]
          [StringLength(5000)]
          public string Content { get; set; }

          [Required]
          public DateTime CreatedAt { get; set; } = DateTime.Now;

          [Required]
          [StringLength(300)]
          public string ImageUrl { get; set; }


     }
}
