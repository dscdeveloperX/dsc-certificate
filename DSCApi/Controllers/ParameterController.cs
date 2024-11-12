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
    [RoutePrefix("api/dsc/parameter")]
    public class ParameterController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<ParameterModel>> Read(int? parameterID = null, bool? parameterActive = null, int page=0, int quantity=0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<ParameterModel> data = new DataModel<ParameterModel>();
            try
            {
                data.Data = await new ParameterModule().Read(parameterID, parameterActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<ParameterModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [HttpGet]
        [Route("count")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<long>> Count()
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data =  new List<long>() { await new ParameterModule().Count() };
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<long>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


        [HttpGet]
        [Route("parameter-company-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<ParameterModel>> ParameterCompanyRead(string companyID = null, bool? parameterActive = null, int page=0, int quantity=0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<ParameterModel> data = new DataModel<ParameterModel>();
            try
            {
                IEnumerable<ParameterModel> Data = await new ParameterModule().ParameterCompanyRead(companyID, parameterActive, page, quantity);
                //data.Data = await new ParameterModule().ParameterCompanyRead(companyID, parameterActive, page, quantity);
                data.Data = Data;
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<ParameterModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }



        [HttpPost]
        [Route("create")]
        public async Task<DataModel<string>> Create([FromBody] ParameterModel ParameterModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new ParameterModule().Create(ParameterModel);
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
        public async Task<DataModel<string>> Update([FromBody] ParameterModel ParameterModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new ParameterModule().Update(ParameterModel);
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
                await new ParameterModule().Delete(id);
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