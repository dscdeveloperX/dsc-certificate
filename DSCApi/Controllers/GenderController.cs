using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Management;
using DSCApi.Models;
using DSCApi.Modules;
using PDFiumSharp;



namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/gender")]
    public class GenderController : ApiController
    {

        //encargado de realizar la utenticacion y obtencion de token
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginModel loginModel) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            bool isCredentialValid = (loginModel.UserPassword == "son200420052012");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginModel.UserName);
                return Ok(token);
            }
            else {
                return Unauthorized();//status code 401
            }
            
        }



        
        [HttpGet]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<GenderModel>> Read(string genderID = null, bool? genderActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<GenderModel> data = new DataModel<GenderModel>();
            try
            {
                data.Data = await new GenderModule().Read(genderID, genderActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<GenderModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [Authorize]
        [HttpGet]
        [Route("count")]
        [EnableCors("http://localhost:60710", "*", "*")]
        public async Task<DataModel<long>> Count()
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data = new List<long>() { await new GenderModule().Count() };
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
        public async Task<DataModel<string>> Create([FromBody] GenderModel genderModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GenderModule().Create(genderModel);
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
        public async Task<DataModel<string>> Update([FromBody] GenderModel genderModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GenderModule().Update(genderModel);
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
        [Route("delete/{id}")]
        public async Task<DataModel<string>> Delete(string id)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new GenderModule().Delete(id);
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
