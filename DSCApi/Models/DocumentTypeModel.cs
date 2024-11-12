using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentTypeModel
    {
        public string DocumentTypeID { get; set; }
        public string DocumentTypeDescription { get; set; }
        public bool? DocumentTypeActive { get; set; }
    }
}