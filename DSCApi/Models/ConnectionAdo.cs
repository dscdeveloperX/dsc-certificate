using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DSCApi.Models
{
    public class ConnectionAdo
    {
        public string ConnectionDSC
        {
            get { return WebConfigurationManager.ConnectionStrings["ConnectionDSC"].ConnectionString; }
        }
    }
}