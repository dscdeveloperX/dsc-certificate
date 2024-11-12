using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class ProvinceCityModel
    {
        public int? ProvinceID { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public bool? CityActive { get; set; }
        public string ProvinceName { get; set; }
        public bool ProvinceActive { get; set; }

    }
}