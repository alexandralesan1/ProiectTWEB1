using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Domain.Enums;

namespace WebApplication4.Models
{
     public class AdminProfileViewModel
     {
          public string Name { get; set; }
          public string Email { get; set; }
          public UserRole Role { get; set; }  
     }
}