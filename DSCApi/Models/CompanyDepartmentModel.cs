using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class CompanyDepartmentModel
    {
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool? CompanyActive { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool? DepartmentActive { get; set; }

    }
}