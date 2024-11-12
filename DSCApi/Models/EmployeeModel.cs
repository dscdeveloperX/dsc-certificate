using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }
        public string PersonPhoto { get; set; }
        public int? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public DateTime? EmployeeDateEntry { get; set; }
        public DateTime? EmployeeDateExit { get; set; }
        public string EmployeeReason { get; set; }
        public bool? EmployeeActive { get; set; }
        
    }
}