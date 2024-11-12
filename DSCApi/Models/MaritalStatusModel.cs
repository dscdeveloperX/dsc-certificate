using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class MaritalStatusModel
    {
        public string MaritalStatusID { get; set; }
        public string MaritalStatusDescription { get; set; }
        public bool? MaritalStatusActive { get; set; }
    }
}