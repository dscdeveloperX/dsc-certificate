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
    [RoutePrefix("api/dsc/company-person")]
    public class CompanyPersonController : ApiController
    {
        [HttpGet]
        //[Route("read/{companyPersonID:int?}/{companyPersonActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<CompanyPersonModel>> Get(int? companyPersonID = null, bool? companyPersonActive = null)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<CompanyPersonModel> data = new DataModel<CompanyPersonModel>();
            try
            {
                data.Data = await new CompanyPersonModule().Read(companyPersonID, companyPersonActive);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<CompanyPersonModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }



        [HttpPost]
        [Route("create")]
        public async Task<DataModel<string>> Create([FromBody] CompanyPersonModel CompanyPersonModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new CompanyPersonModule().Create(CompanyPersonModel);
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
        public async Task<DataModel<string>> Update([FromBody] CompanyPersonModel CompanyPersonModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new CompanyPersonModule().Update(CompanyPersonModel);
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
                await new CompanyPersonModule().Delete(id);
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