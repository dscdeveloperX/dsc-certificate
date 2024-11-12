using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class EmailConfigModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsBodyHtml { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}