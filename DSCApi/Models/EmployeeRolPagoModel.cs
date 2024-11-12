using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class EmployeeRolPagoModel
    {
        public int EmployeeID { get; set; }
        public DateTime EmployeeDateEntry { get; set; }
        public string PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string PersonSignatureImage { get; set; }
        public string PersonPhoto { get; set; }
        public string PersonEmail { get; set; }
        public int CompanyID { get; set; }
        public string CompanyRuc { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyPhoto { get; set; }
        public string CompanyUrlVerification { get; set; }
        public string CompanyCodeQrVerification { get; set; }
    }
}