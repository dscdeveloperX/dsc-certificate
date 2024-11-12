using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DepartmentModel
    {
        public int? DepartmentID { get; set; }
        public string CompanyID { get; set; }
        public string DepartmentName { get; set; }
        public bool? DepartmentActive { get; set; }
    }
}