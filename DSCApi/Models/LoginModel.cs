using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}