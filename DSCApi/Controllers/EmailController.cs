using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using DSCApi.Modules;
using DSCApi.Models;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/contact")]
    public class EmailController : ApiController
    {


        /*
          EmailModel emailModel = new EmailModel() {
            DisplayName= "DSC Freenlace",
            Subject= "Asunto Dsc Freelance",
            Body= "<h1>Titulo del mensaje</h1><p>el contenido de este mensaje es ahora</p>"
            };
         */

        [HttpPost]
        [Route("send-email-a")]
        public async Task<DataModel<string>> SendEmailA([FromBody] EmailModel emailModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            string[] lista = new string[]{ 
                "darwinrodolfosanchezcorrea@outlook.es", 
                "darwinrodolfosanchezcorrea@outlook.com", 
                "dscdeveloper@outlook.com", 
                "codigodarwin7@gmail.com", 
                "codigodarwin@outlook.com", 
                "darwinrsc@outlook.es", 
                "tecnologiaescarlata@gmail.com", 
                "dsc@facefocuscontrol.com", 
                "darwinrodolfosanchezcorrea@gmail.com", 
                "codigodarwin7@gmail.com", 
                "codigodarwin@outlook.com",
            "darwinrodolfosanchezcorrea@outlook.es",
                "darwinrodolfosanchezcorrea@outlook.com",
                "dscdeveloper@outlook.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
                "darwinrsc@outlook.es",
                "tecnologiaescarlata@gmail.com",
                "dsc@facefocuscontrol.com",
                "darwinrodolfosanchezcorrea@gmail.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
            "darwinrodolfosanchezcorrea@outlook.es",
                "darwinrodolfosanchezcorrea@outlook.com",
                "dscdeveloper@outlook.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
                "darwinrsc@outlook.es",
                "tecnologiaescarlata@gmail.com",
                "dsc@facefocuscontrol.com",
                "darwinrodolfosanchezcorrea@gmail.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
            "darwinrodolfosanchezcorrea@outlook.es",
                "darwinrodolfosanchezcorrea@outlook.com",
                "dscdeveloper@outlook.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
                "darwinrsc@outlook.es",
                "tecnologiaescarlata@gmail.com",
                "dsc@facefocuscontrol.com",
                "darwinrodolfosanchezcorrea@gmail.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
            "darwinrodolfosanchezcorrea@outlook.es",
                "darwinrodolfosanchezcorrea@outlook.com",
                "dscdeveloper@outlook.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com",
                "darwinrsc@outlook.es",
                "tecnologiaescarlata@gmail.com",
                "dsc@facefocuscontrol.com",
                "darwinrodolfosanchezcorrea@gmail.com",
                "codigodarwin7@gmail.com",
                "codigodarwin@outlook.com"};
            for(int i=0;i< lista.Length;i++)
            {
                await Task.Delay(10000);
            try
            {
                responseData.Data = await new ContactModule().SendMail(
                    //entia todos los parametros por defaul + email para ser enviado por medio de email empresarial
                    new ConfigEmail().AccountWebSite(lista[i]),
                    //estos lleno el cuerpo del mensaje
                    emailModel);
                responseData.State = DataState.ok;
                responseData.Message = "Mensaje enviado exitosamente";

            }
            catch (Exception ex)
            {
                responseData.Data = new string[] { };
                responseData.State = DataState.error;
                responseData.Message = lista[i] + " | " +ex.Message;
                    break;
            }
            
        }
            return responseData;


        }

    }
}
