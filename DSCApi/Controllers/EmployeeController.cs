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
    [RoutePrefix("api/dsc/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<EmployeeModel>> Read(int? departmentID = null, bool? departmentActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<EmployeeModel> data = new DataModel<EmployeeModel>();
            try
            {
                data.Data = await new EmployeeModule().Read(departmentID, departmentActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<EmployeeModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [HttpGet]
        [Route("read-search")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<EmployeeModel>> ReadSearch(string personName, int companyID, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<EmployeeModel> data = new DataModel<EmployeeModel>();
            try
            {
                data.Data = await new EmployeeModule().ReadSearch(personName, companyID, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<EmployeeModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [HttpGet]
        [Route("count")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<long>> Count(string personName, int companyID)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data = new List<long>() { await new EmployeeModule().Count(personName, companyID) };
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
        [AllowAnonymous]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<string>> Create([FromBody] EmployeeModel EmployeeModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //crear empleado
                await new EmployeeModule().Create(EmployeeModel);
                //crear usuario
                await new UserModule().CreateAutomatic(
                    new UserAutomaticModel() { 
                    CedulaRuc = EmployeeModel.PersonID, 
                    UserPassword = EncryptModule.GeneratePassword() }
                    );
                //
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
        public async Task<DataModel<string>> Update([FromBody] EmployeeModel EmployeeModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new EmployeeModule().Update(EmployeeModel);
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
        [Route("delete/{companyID:int}/{employeeID:int}")]
        public async Task<DataModel<string>> Delete(int companyID, int employeeID)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new EmployeeModule().Delete(companyID, employeeID);
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