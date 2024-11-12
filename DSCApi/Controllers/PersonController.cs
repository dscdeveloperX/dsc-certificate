using DSCApi.Models;
using DSCApi.Modules;
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

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/person")]
    public class PersonController : ApiController
    {
        [HttpGet]
        //[Route("read/{companyID}/{companyActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public  Task<DataModel<PersonModel>> Read(string personID = null, bool? personActive = null, int page = 0, int quantity = 0)
        {
            throw new Exception();
            /*
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<PersonModel> data = new DataModel<PersonModel>();
            try
            {
                data.Data = await new PersonModule().Read(personID, personActive, page ,quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<PersonModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;
            */
        }

        [HttpGet]
        //[Route("read/{companyID}/{companyActive:bool?}")]
        [Route("read-search")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<PersonModel>> ReadSearch(string personName = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<PersonModel> data = new DataModel<PersonModel>();
            try
            {
                data.Data = await new PersonModule().ReadSearch(personName, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<PersonModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [HttpGet]
        //[Route("read/{companyID}/{companyActive:bool?}")]
        [Route("person-employee-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<PersonEmployeeModel>> PersonEmployeeRead(int companyID)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<PersonEmployeeModel> data = new DataModel<PersonEmployeeModel>();
            try
            {
                data.Data = await new PersonModule().PersonEmployeeRead(companyID);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<PersonEmployeeModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        //NO SE USA
        [HttpGet]
        [Route("count-no")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public  Task<DataModel<long>> Count()
        {
            /*
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data = new List<long>() { await new PersonModule().Count() };
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

            return data;*/
            throw new Exception();
        }

        [HttpGet]
        [Route("count")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<long>> Count(string personName)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data = new List<long>() { await new PersonModule().Count(personName) };
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
        [Route("create-2")]
        public async Task<DataModel<string>> Create([FromBody] PersonModel PersonModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new PersonModule().Create(PersonModel);
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

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<string>> Create()
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //path foto Y Signature
                string pathPhoto = "Uploads/Photo";
                string fileNamePhoto = "";
                string pathSignature = "Uploads/Signature";
                string fileNameSignature = "";
                //todo lo recibido
                System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
                //obtenemos datos campos de form
                System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
                //obtenemos datos files
                HttpFileCollection fileData = httpRequest.Files;
                //-------------------------
                if (fileData.Count > 0)
                {
                    //PHOTO
                    HttpPostedFile filePhoto = fileData["Photo"];//[0]
                    if (filePhoto != null)
                    {
                        if (filePhoto.ContentLength <= 2097152)
                        {
                            //5mg 5242880 | 2mg 2097152
                            if (filePhoto.ContentType == "image/jpeg" || filePhoto.ContentType == "image/png")
                            {
                                fileNamePhoto = $"photo_{new Random().Next(1000)}_{Path.GetFileName(filePhoto.FileName)}";
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathPhoto), fileNamePhoto);
                                filePhoto.SaveAs(path);
                            }
                            else
                            {
                                throw new Exception("Foto: no es una imagen");
                            }
                        }
                        else
                        {
                            throw new Exception("Foto: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Foto: no existe");
                    }
                    //SIGNATURE
                    HttpPostedFile fileSignature = fileData["Signature"];//[0]
                    if (fileSignature != null)
                    {
                        if (fileSignature.ContentLength <= 2097152)
                        {
                            //5mg 5242880 | 2mg 2097152
                            if (fileSignature.ContentType == "image/jpeg" || fileSignature.ContentType == "image/png")
                            {
                                fileNameSignature = $"signature_{new Random().Next(1000)}_{Path.GetFileName(fileSignature.FileName)}";
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathSignature), fileNameSignature);
                                fileSignature.SaveAs(path);
                            }
                            else
                            {
                                throw new Exception("Firma: no es una imagen");
                            }
                        }
                        else
                        {
                            throw new Exception("Firma: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Firma: no existe");
                    }
                }
                else
                {
                    throw new Exception("archivos de imágenes no existen");
                }

                PersonModel persona = new PersonModel();
                //
                persona.PersonID = formData["PersonID"].ToString();
                persona.PersonPhoto = pathPhoto + "/" + fileNamePhoto;
                persona.PersonSignatureImage = pathSignature + "/" + fileNameSignature;
                persona.PersonEmail = formData["PersonEmail"].ToString();
                persona.ProvinceID = Convert.ToInt32(formData["ProvinceID"]);
                persona.CityID = Convert.ToInt32(formData["CityID"]);
                persona.PersonName = formData["PersonName"].ToString();
                persona.PersonSurname = formData["PersonSurname"].ToString();
                persona.PersonDateOfBirth = Convert.ToDateTime(formData["PersonDateOfBirth"]);
                persona.PersonPhone = formData["PersonPhone"].ToString();
                persona.GenderID = formData["GenderID"].ToString();
                persona.MaritalStatusID = formData["MaritalStatusID"].ToString();
                persona.PersonActive = Convert.ToBoolean(formData["PersonActive"]);
                //-------------------------
                ///
                await new PersonModule().Create(persona);
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
        [Route("update-photo")]
        public async Task<DataModel<string>> UpdatePhoto()
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //path foto Y Signature
                //string pathPhoto = "Uploads/Photo";
                string fileNamePhoto = "";
                //todo lo recibido
                System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
                //obtenemos datos campos de form
                System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
                //obtenemos datos files
                HttpFileCollection fileData = httpRequest.Files;
                //-------------------------
                if (fileData.Count > 0)
                {
                    //PHOTO
                    HttpPostedFile filePhoto = fileData["Photo"];//[0]
                    if (filePhoto != null)
                    {
                        if (filePhoto.ContentLength <= 2097152)
                        {
                            //5mg 5242880 | 2mg 2097152
                            if (filePhoto.ContentType == "image/jpeg" || filePhoto.ContentType == "image/png")
                            {
                                string PersonPhoto = formData["PersonPhoto"].ToString();
                                fileNamePhoto = $"photo_{new Random().Next(1000)}_{Path.GetFileName(filePhoto.FileName)}";
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/"), PersonPhoto);
                                filePhoto.SaveAs(path);
                            }
                            else
                            {
                                throw new Exception("Foto: no es una imagen");
                            }
                        }
                        else
                        {
                            throw new Exception("Foto: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Foto: no existe");
                    }
                }
                else
                {
                    throw new Exception("archivos de imágenes no existen");
                }
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
        [Route("update-signature")]
        public async Task<DataModel<string>> UpdateSignature()
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //path foto Y Signature
                //string pathSignature = "Uploads/Signature";
                string fileNameSignature = "";
                //todo lo recibido
                System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
                //obtenemos datos campos de form
                System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
                //obtenemos datos files
                HttpFileCollection fileData = httpRequest.Files;
                //-------------------------
                if (fileData.Count > 0)
                {
                    //PHOTO
                    HttpPostedFile fileSignature = fileData["Signature"];//[0]
                    if (fileSignature != null)
                    {
                        if (fileSignature.ContentLength <= 2097152)
                        {
                            //5mg 5242880 | 2mg 2097152
                            if (fileSignature.ContentType == "image/jpeg" || fileSignature.ContentType == "image/png")
                            {
                                string PersonSignature = formData["PersonSignature"].ToString();
                                fileNameSignature = $"signature_{new Random().Next(1000)}_{Path.GetFileName(fileSignature.FileName)}";
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/"), PersonSignature);
                                fileSignature.SaveAs(path);
                            }
                            else
                            {
                                throw new Exception("Firma: no es una imagen");
                            }
                        }
                        else
                        {
                            throw new Exception("Firma: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Firma: no existe");
                    }
                }
                else
                {
                    throw new Exception("archivos de imágenes no existen");
                }
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
        public async Task<DataModel<string>> Update([FromBody] PersonModel PersonModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new PersonModule().Update(PersonModel);
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
                await new PersonModule().Delete(id);
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