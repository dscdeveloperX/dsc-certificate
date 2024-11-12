using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string RoleID { get; set; }
        public string UserAlias { get; set; }
        public string UserPassword { get; set; }
        public string UserRef { get; set; }
        public bool UserActive { get; set; }
              
    }

    public class UserAutomaticModel {
        public string UserPassword;
        public string CedulaRuc;
    }
}