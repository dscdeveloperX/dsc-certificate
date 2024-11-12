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
    [RoutePrefix("api/dsc/department")]
    public class DepartmentController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DepartmentModel>> Read(int? departmentID = null, bool? departmentActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DepartmentModel> data = new DataModel<DepartmentModel>();
            try
            {
                data.Data = await new DepartmentModule().Read(departmentID, departmentActive,page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DepartmentModel>() { };
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
                data.Data = new List<long>() { await new DepartmentModule().Count() };
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


        [HttpPost]
        [Route("create")]
        public async Task<DataModel<string>> Create([FromBody] DepartmentModel DepartmentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new DepartmentModule().Create(DepartmentModel);
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
        public async Task<DataModel<string>> Update([FromBody] DepartmentModel DepartmentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new DepartmentModule().Update(DepartmentModel);
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
                await new DepartmentModule().Delete(id);
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