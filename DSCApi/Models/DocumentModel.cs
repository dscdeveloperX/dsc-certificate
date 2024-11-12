using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentModel
    {
        public int? DocumentID { get; set; }
        public int GroupDocumentID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCode { get; set; }
        public string PersonID { get; set; }
        public DateTime? DocumentDateCreation { get; set; }
        public bool? DocumentActive { get; set; }
    }
}