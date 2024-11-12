using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class OccupationModel
    {
        public string OccupationID { get; set; }
        public string OccupationDescription { get; set; }
        public bool? OccupationActive { get; set; }
  
    }
}