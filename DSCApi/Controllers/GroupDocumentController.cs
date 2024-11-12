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
    [RoutePrefix("api/dsc/group-document")]
    public class GroupDocumentController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<GroupDocumentModel>> Read(int? groupDocumentID = null, bool? groupDocumentActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<GroupDocumentModel> data = new DataModel<GroupDocumentModel>();
            try
            {
                data.Data = await new GroupDocumentModule().Read(groupDocumentID, groupDocumentActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<GroupDocumentModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("group-document-company-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<GroupDocumentModel>> GroupDocumentCompanyGet(string groupDocumentID = null, bool? groupDocumentActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<GroupDocumentModel> data = new DataModel<GroupDocumentModel>();
            try
            {
                data.Data = await new GroupDocumentModule().GroupDocumentCompanyRead(groupDocumentID, groupDocumentActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<GroupDocumentModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }



        [HttpPost]
        [Route("create")]
        public async Task<DataModel<string>> Create([FromBody] GroupDocumentModel GroupDocumentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GroupDocumentModule().Create(GroupDocumentModel);
                responseData.Data = new string[] { };
                responseData.State = DataState.ok;
                responseData.Message = "Mensaje enviado exitosamente";

            }
            catch (Exception ex)
            {
                responseData.Data = new string[] { };
                responseData.State = DataState.error;
                responseData.Message = ex.Message;
            }

            return responseData;


        }



        [HttpPut]
        [Route("update")]
        public async Task<DataModel<string>> Update([FromBody] GroupDocumentModel GroupDocumentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GroupDocumentModule().Update(GroupDocumentModel);
                responseData.Data = new string[] { };
                responseData.State = DataState.ok;
                responseData.Message = "Mensaje enviado exitosamente";

            }
            catch (Exception ex)
            {
                responseData.Data = new string[] { };
                responseData.State = DataState.error;
                responseData.Message = ex.Message;
            }

            return responseData;


        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<DataModel<string>> Delete(int id)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GroupDocumentModule().Delete(id);
                responseData.Data = new string[] { };
                responseData.State = DataState.ok;
                responseData.Message = "Mensaje enviado exitosamente";

            }
            catch (Exception ex)
            {
                responseData.Data = new string[] { };
                responseData.State = DataState.error;
                responseData.Message = ex.Message;
            }

            return responseData;


        }


    }
}