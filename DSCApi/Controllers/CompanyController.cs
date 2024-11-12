using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DSCApi.Models;
using DSCApi.Modules;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/company")]
    public class CompanyController : ApiController
    {

        [HttpGet]
        //[Route("read/{companyID}/{companyActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<CompanyModel>> Read(int? companyID = null, bool? companyActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<CompanyModel> data = new DataModel<CompanyModel>();
            try
            {
                data.Data = await new CompanyModule().Read(companyID, companyActive, page, quantity );
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<CompanyModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


        [HttpGet]
        [Route("read-full")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<CompanyModel>> ReadFull(int? companyID = null, bool? companyActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<CompanyModel> data = new DataModel<CompanyModel>();
            try
            {
                data.Data = await new CompanyModule().ReadFull(companyID, companyActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<CompanyModel>() { };
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
                data.Data = new List<long>() { await new CompanyModule().Count() };
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
        [AllowAnonymous]
        [Route("create")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<string>> Create([FromBody] CompanyModel CompanyModel)
        {
            
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //crear compania
                await new CompanyModule().Create(CompanyModel);
                //crear usuario
                await new UserModule().CreateAutomatic(
                    new UserAutomaticModel()
                    {
                        CedulaRuc = CompanyModel.CompanyRuc,
                        UserPassword = EncryptModule.GeneratePassword()
                    }
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
        public async Task<DataModel<string>> Update([FromBody] CompanyModel CompanyModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new CompanyModule().Update(CompanyModel);
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
                await new CompanyModule().Delete(id);
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



/*
        [HttpGet]
        //[Route("company-department-read/{companyID}/{companyActive:bool?}")]
        [Route("company-department-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<CompanyDepartmentModel>> CompanyDepartmentGet(string companyID = null, bool? companyActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<CompanyDepartmentModel> data = new DataModel<CompanyDepartmentModel>();
            try
            {
                data.Data = await new CompanyModule().CompanyDepartmentRead(companyID, companyActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<CompanyDepartmentModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }
        */