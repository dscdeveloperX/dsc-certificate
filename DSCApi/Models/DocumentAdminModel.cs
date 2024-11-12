using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DocumentAdminModel
    {
        public int DocumentID { get; set; }
        public int DocumentGroupID { get; set; }
        public string DocumentType { get; set; }
        public string PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentEmailSend { get; set; }
        public bool DocumentEmailSendState { get; set; }
        public DateTime? DocumentDateEmailSend { get; set; }
        public DateTime DocumentDateCreation { get; set; }
    }
}