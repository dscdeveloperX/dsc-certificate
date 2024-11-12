using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class CompanyPersonModel
    {
        public int? CompanyPersonID { get; set; }
        public string CompanyID { get; set; }
        public string PersonID { get; set; }
        public bool? PersonActive { get; set; }
    }
}