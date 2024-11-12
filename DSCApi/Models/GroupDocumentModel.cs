using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    //ERROR ERROR ESTE NO ES
    public class GroupDocumentModel
    {
        public int? GroupDocumentID { get; set; }
        public int CompanyID { get; set; }
        public string GroupDocumentType { get; set; }
        public DateTime? GroupDocumentDate { get; set; }
        public string GroupDocumentDescription { get; set; }
        public bool? GroupDocumentActive { get; set; }
        
    }
}