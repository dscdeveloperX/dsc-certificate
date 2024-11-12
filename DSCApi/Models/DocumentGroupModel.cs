using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentGroupModel
    {
        public int? DocumentGroupID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string DocumentGroupType { get; set; }
        public DateTime? DocumentGroupDate { get; set; }
        public string DocumentGroupDescription { get; set; }
        public bool? DocumentGroupActive { get; set; }
    }
}