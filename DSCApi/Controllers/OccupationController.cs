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
    [RoutePrefix("api/dsc/occupation")]
    public class OccupationController : ApiController
    {
        [HttpGet]
        //[Route("read/{occupationID:int?}/{occupationActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<OccupationModel>> Read(string occupationID = null, bool? occupationActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<OccupationModel> data = new DataModel<OccupationModel>();
            try
            {
                data.Data = await new OccupationModule().Read(occupationID, occupationActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<OccupationModel>() { };
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
                data.Data = new List<long>() { await new OccupationModule().Count() };
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
        public async Task<DataModel<string>> Create([FromBody] OccupationModel occupationModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new OccupationModule().Create(occupationModel);
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
        public async Task<DataModel<string>> Update([FromBody] OccupationModel occupationModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new OccupationModule().Update(occupationModel);
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
                await new OccupationModule().Delete(id);
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
