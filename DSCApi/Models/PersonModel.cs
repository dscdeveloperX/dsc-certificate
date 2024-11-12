using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class PersonModel
    {
        public string PersonID { get; set; }
        public string PersonSignatureImage { get; set; }
        public string PersonPhoto { get; set; }
        public string PersonEmail { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public DateTime PersonDateOfBirth { get; set; }
        public string PersonPhone { get; set; }
        public string GenderID { get; set; }
        public string GenderDescription { get; set; }
        public string MaritalStatusID { get; set; }
        public string MaritalStatusDescription { get; set; }
        public bool? PersonActive { get; set; }
        
    }
}