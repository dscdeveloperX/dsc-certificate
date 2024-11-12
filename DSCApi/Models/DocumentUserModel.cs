using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentUserModel
    {
        public int DocumentID { get; set; }
        public int DocumentGroupID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentGroupDescription { get; set; }
        public DateTime DocumentGroupDate { get; set; }
    }
}