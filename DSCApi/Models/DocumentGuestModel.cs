using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentGuestModel
    {
        public int DocumentID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeDescription { get; set; }
        public string PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string DocumentCode { get; set; }
        public bool EmployeeActive { get; set; }
        public DateTime DocumentDateCreation { get; set; }
    }
}