using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Modules
{
    public class DocumentXmlModel
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string DocumentEmailSend { get; set; }
        public string DocumentGroupDescription { get; set; }
        public string DocumentCode { get; set; }
    }
}