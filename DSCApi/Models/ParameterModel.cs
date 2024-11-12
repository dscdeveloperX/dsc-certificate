using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class ParameterModel
    {
        public int? ParameterID { get; set; }
        public string CompanyID { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterType { get; set; }
        public bool? ParameterActive { get; set; }
        public int? RowsCount { get; set; }

    }
}