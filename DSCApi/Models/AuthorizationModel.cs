using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace DSCApi.Models
{
    public class AuthorizationModel
    {
        public string UserName { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
        public string Message { get; set; } 
    }
}