using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using DocumentFormat.OpenXml.Spreadsheet;
using DSCApi.Models;
using DSCApi.Modules;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/user")]
    public class UserController : ApiController
    {
        //encargado de realizar la utenticacion y obtencion de token
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel loginModel)
        {
            try {
            

            //pendiente validaciones loginModel
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState); 
            }
            //si es correcta la validacion de usuario
            string rol = await new UserModule().Login(loginModel);//valida usuario password
            bool isCredentialValid = rol.Length > 0? true: false;
                //
            if (isCredentialValid)
            {

                return Ok(new AuthorizationModel()
                {
                    Token = TokenGenerator.GenerateTokenJwt(loginModel.UserName),
                    Message ="Validación Exitosa",
                    Rol=rol,
                    UserName =loginModel.UserName
                });
            }
                throw new Exception("Usuario o contraseña inválida");
            /*else
            {
                return Unauthorized();//status code 401
            }*/

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Unauthorized, new AuthorizationModel()
                {
                    Token = "",
                    Message = ex.Message,
                    Rol = "",
                    UserName = ""
                });
             
            }

        }

        [Authorize]
        [HttpPost]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<UserModel>> Read(int? userID = null, bool? userActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<UserModel> data = new DataModel<UserModel>();
            try
            {
                page = 1;quantity = 1000;
                data.Data = await new UserModule().Read(userID, userActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<UserModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


    }
}
