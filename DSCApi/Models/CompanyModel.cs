using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; }
        public string CompanyRuc { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyPhoto { get; set; }
        public string CompanyUrlVerification { get; set; }
        public string CompanyCodeQrVerification { get; set; }
        public bool? CompanyActive { get; set; }

    }
}