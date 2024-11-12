using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }
        public int ProvinceID { get; set; }
        public string CityName { get; set; }
        public bool? CityActive { get; set; }
    }
}