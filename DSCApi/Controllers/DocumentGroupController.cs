using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using DSCApi.Models;
using DSCApi.Modules;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/document-group")]
    public class DocumentGroupController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DocumentGroupModel>> Read(int? companyID = null, string documentGroupType = "", int? documentGroupDateYear = null, bool? documentGroupActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DocumentGroupModel> data = new DataModel<DocumentGroupModel>();
            try
            {
                data.Data = await new DocumentGroupModule().DocumentGroupCompanyRead(companyID, documentGroupType, documentGroupDateYear, documentGroupActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DocumentGroupModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }
            return data;

        }

    }
}
