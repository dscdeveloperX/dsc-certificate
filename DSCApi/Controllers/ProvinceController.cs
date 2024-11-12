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
    [RoutePrefix("api/dsc/province")]
    public class ProvinceController : ApiController
    {

        [HttpGet]
        [Route("read/{provinceID:int?}/{provinceActive:bool?}")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<ProvinceModel>> Read(int? provinceID = null, bool? provinceActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<ProvinceModel> data = new DataModel<ProvinceModel>();
            try
            {
                data.Data = await new ProvinceModule().Read(provinceID, provinceActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<ProvinceModel>() { };
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
                data.Data = new List<long>() { await new ProvinceModule().Count() };
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
        public async Task<DataModel<string>> Create([FromBody] ProvinceModel provinceModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new ProvinceModule().Create(provinceModel);
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
        public async Task<DataModel<string>> Update([FromBody] ProvinceModel provinceModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new ProvinceModule().Update(provinceModel);
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
                await new ProvinceModule().Delete(id);
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


        [HttpGet]
        [Route("province-city-read/{provinceID:int?}/{provinceActive:bool?}")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<ProvinceCityModel>> CityProvinceGet(int? provinceID = null, bool? provinceActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<ProvinceCityModel> data = new DataModel<ProvinceCityModel>();
            try
            {
                data.Data = await new ProvinceModule().ProvinceCityRead(provinceID, provinceActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<ProvinceCityModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


    }
}
