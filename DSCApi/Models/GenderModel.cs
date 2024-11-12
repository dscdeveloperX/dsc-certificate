using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class GenderModel
    {
        public string GenderID { get; set; }
        public string GenderDescription { get; set; }
        public bool? GenderActive { get; set; }
        
    }
}