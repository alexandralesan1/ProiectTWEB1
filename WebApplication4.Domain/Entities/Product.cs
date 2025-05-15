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
    public class DBProductTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        [Required]
        public ProductBrand Brand { get; set; }

        [Required]
        public ProductCountry Country { get; set; }

        [Required]
        public ProductSpecialCategory SpecialCategory { get; set; }

    }
}
