using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DSCApi.Models;
using DSCApi.Modules;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PDFiumSharp;
using Font = iTextSharp.text.Font;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net.Http.Headers;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/document")]
    public class DocumentController : ApiController
    {
        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DocumentModel>> Read(int? documentID = null, bool? documentActive = null, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DocumentModel> data = new DataModel<DocumentModel>();
            try
            {
                data.Data = await new DocumentModule().Read(documentID, documentActive, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DocumentModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }

        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("document-admin-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DocumentAdminModel>> DocumentAdminRead(int documentGroupID, int page = 0, int quantity = 0)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DocumentAdminModel> data = new DataModel<DocumentAdminModel>();
            try
            {
                data.Data = await new DocumentModule().DocumentAdminRead(documentGroupID, page, quantity);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DocumentAdminModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


        [HttpGet]
        //[Route("read/{departmentID:int?}/{departmentActive:bool?}")]
        [Route("document-guest-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DocumentGuestModel>> DocumentGuestRead(string documentCode)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DocumentGuestModel> data = new DataModel<DocumentGuestModel>();
            try
            {
                data.Data = await new DocumentModule().DocumentGuestRead(documentCode);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DocumentGuestModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }





        [HttpGet]
        [Route("document-user-read")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<DocumentUserModel>> DocumentUserRead(int companyID, string documentType, int documentGroupDateYear)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<DocumentUserModel> data = new DataModel<DocumentUserModel>();
            try
            {
                data.Data = await new DocumentModule().DocumentUserRead(companyID, documentType, documentGroupDateYear);
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<DocumentUserModel>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }



        [HttpGet]
        [Route("count/{documentGroupID:int}")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<long>> Count(int documentGroupID)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<long> data = new DataModel<long>();
            try
            {
                data.Data = new List<long>() { await new DocumentModule().Count(documentGroupID) };
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
        public async Task<DataModel<string>> Create([FromBody] DocumentModel DocumentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new DocumentModule().Create(DocumentModel);
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
        public async Task<DataModel<string>> Update([FromBody] DocumentModel DocumentModel)
        {
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                await new DocumentModule().Update(DocumentModel);
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
                await new DocumentModule().Delete(id);
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
        [Route("update-xml")]
        public async Task<DataModel<string>> UpdateXml([FromBody] List<int> data)
        {
            //GENERO XML 
            StringBuilder dataXml = new StringBuilder();
            StringBuilder dataXmlSelect = new StringBuilder();
            dataXml.Append("<?xml version=\"1.0\" standalone=\"yes\"?><root>");
            dataXmlSelect.Append("<?xml version=\"1.0\" standalone=\"yes\"?><root>");
            for (int i = 0; i < data.Count; i++)
            {
                dataXml.Append($"<item DocumentID=\"{data[i]}\" DocumentDateEmailSend=\"{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}\" DocumentEmailSendState=\"1\" />");
                dataXmlSelect.Append($"<item><DocumentID>{data[i]}</DocumentID></item>");
            }
            dataXml.Append("</root>");
            dataXmlSelect.Append("</root>");
            //
            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //obtengo datos para eliminar
                List<DocumentXmlModel> documentXml = await new DocumentModule().SelectXML(dataXmlSelect.ToString()) as List<DocumentXmlModel>;
                //actualizo documentos de la base de datos
                await new DocumentModule().UpdateXML(dataXml.ToString());
                for (int i = 0; i < documentXml.Count; i++)
                {
                    try
                    {

                        //await new DocumentModule().DeleteDocument(documentXml[i].DocumentCode);
                        await new ContactModule().SendMailDocument(
                        //entia todos los parametros por defaul + email para ser enviado por medio de email empresarial
                        new ConfigEmail().AccountWebSite(documentXml[i].DocumentEmailSend),
                        //estos lleno el cuerpo del mensaje
                        new EmailModel()
                        {
                            DisplayName = "Firma Electrónica",
                            Subject = documentXml[i].DocumentGroupDescription,
                            Body = $"<p>Estimado/a</p><br /><p><strong>{documentXml[i].PersonSurname} {documentXml[i].PersonName}</strong>,</p><br /><p>Ha concluido el proceso de firma electrónica de {documentXml[i].DocumentGroupDescription}, adjunto encontrará su documento firmado.</p><br /><p>Salidos Cordiales,</p><br /><p>Talento Humano.</p><br /><hr /><p><strong>NOTA: Este e-mail se ha generado automáticamente. Por favor no responder.</strong></p>"
                        }
                        ,
                        //envio de nobmre del documento
                        documentXml[i].DocumentCode                       
                        );
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                    responseData.Data = new string[] { };
                    responseData.State = DataState.ok;
                    responseData.Message = "Mensaje enviado exitosamente";
                }
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
        [Route("delete-xml/{documentGroupID:int}")]
        public async Task<DataModel<string>> DeleteXml([FromBody] List<int> data, [FromUri] int documentGroupID)
        {
            //GENERO XML 
            StringBuilder dataXml = new StringBuilder();
            dataXml.Append("<?xml version=\"1.0\" standalone=\"yes\"?><root>");
            for (int i = 0; i < data.Count; i++)
            {
                //<item><id>2</id></item><item><id>3</id></item>
                dataXml.Append($"<item><DocumentID>{data[i]}</DocumentID></item>");
            }
            dataXml.Append("</root>");

            DataModel<string> responseData = new DataModel<string>();
            try
            {
                //obtengo datos para eliminar
                List<DocumentXmlModel> documentXml = await new DocumentModule().SelectXML(dataXml.ToString()) as List<DocumentXmlModel>;
                //eliminar documentos de la base de datos
                await new DocumentModule().DeleteXML(dataXml.ToString(), documentGroupID);
                for (int i = 0; i < documentXml.Count; i++)
                {
                    await new DocumentModule().DeleteDocument(documentXml[i].DocumentCode);
                }
                //enviar objeto con respuesta
                responseData.Data = new string[] { };
                responseData.State = DataState.ok;
                responseData.Message = "Proceso realizado con éxito";
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
        [Route("xml-generate")]
        public async Task<string> XmlGenerate()
        {
            try
            {

                string xml = "";
                //todo lo recibido
                System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
                //obtenemos datos campos de form text
                System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
                //obtenemos coleccion de archivos
                HttpFileCollection fileData = httpRequest.Files;

                if (fileData.Count > 0)
                {
                    //file
                    HttpPostedFile file = fileData["DocumentFile"];
                    if (file != null)
                    {
                        if (file.ContentLength <= 5242880)//5mg 5242880 | 2mg 2097152
                        {
                            if (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || file.ContentType.ToLower() == "application/vnd.ms-excel.sheet.macroenabled.12")
                            {
                                //
                                if (formData.Count > 0)
                                {
                                    string xmlType = formData["DocumentType"].ToString();
                                    switch (xmlType)
                                    {
                                        case "CERT-619":
                                            xml = await XmlGenerateRolPago(file.InputStream);
                                            break;
                                        case "CERT-410":
                                            //XmlGenerateRolPago(file.InputStream);
                                            break;
                                        default:
                                            throw new Exception("No especifica el tipo de certificado a generar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("No existen datos de texto");
                                }
                            }
                            else
                            {
                                throw new Exception("Archivo: no es una hoja de cálculo ");
                            }
                        }
                        else
                        {
                            throw new Exception("Archivo: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Arhivo: no existe");
                    }
                }
                else
                {
                    throw new Exception("archivos de imágenes no existen");
                }
                return xml;
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }


        private async Task<string> XmlGenerateRolPago(Stream stream)
        {
            /*//todo lo recibido
            System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
            //obtenemos datos campos de form text
            System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
            //obtenemos coleccion de archivos
            HttpFileCollection fileData = httpRequest.Files;
            HttpPostedFile file = fileData["XmlDocument"];*/
            //
            int beginRow = 9;
            int maxRows = 1000 + beginRow;
            string sheetName = "1";
            //int column = 1000;
            int empty = 0;
            string[] ingresos = new string[] { "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD" };
            string[] ingresosTitle = new string[] { "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD" };
            string[] descuentos = new string[] { "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS" };
            string[] descuentosTitle = new string[] { "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS" };
            //
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                string valor222 = DocumentModule.CellValue(spreadsheetDocument, sheetName, "AF", 6);
                //guardo titulos de rubors ingresos
                for (int i = 0; i < ingresosTitle.Length; i++)
                {
                    ingresosTitle[i] = $"{DocumentModule.CellValue(spreadsheetDocument, sheetName, ingresosTitle[i], 4)} {DocumentModule.CellValue(spreadsheetDocument, sheetName, ingresosTitle[i], 5)} {DocumentModule.CellValue(spreadsheetDocument, sheetName, ingresosTitle[i], 6)}";
                }
                //guardo titulos de rubors descuentos
                for (int i = 0; i < descuentosTitle.Length; i++)
                {
                    descuentosTitle[i] = $"{DocumentModule.CellValue(spreadsheetDocument, sheetName, descuentosTitle[i], 4)} {DocumentModule.CellValue(spreadsheetDocument, sheetName, descuentosTitle[i], 5)} {DocumentModule.CellValue(spreadsheetDocument, sheetName, descuentosTitle[i], 6)}";
                }
                //obtenemos datos campos de form
                //obtenemos datos files
                StringBuilder xmlDocument = new StringBuilder();
                xmlDocument.Append(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>");
                xmlDocument.Append("<documentos>");
                for (int row = beginRow; row < maxRows; row++)
                {
                    //pregunta si contenido en cela A es vacio
                    string cellContent = DocumentModule.CellValue(spreadsheetDocument, sheetName, "A", row);
                    if (cellContent == string.Empty)
                    {
                        //si hay 10 espacios vacios termino bucle
                        if (empty > 10)
                        {
                            break;
                        }
                        //paso al siguiente blucle
                        empty++;
                        continue;
                    }
                    empty = 0;


                    decimal sueldo_base = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "L", row));
                    decimal dias_base = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "M", row));
                    decimal sueldo = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "R", row));
                    decimal hrs_cantidad_25 = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "O", row));
                    decimal hrs_cantidad_50 = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "P", row));
                    decimal hrs_cantidad_100 = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "Q", row));
                    decimal hrs_valor_25 = Math.Round((hrs_cantidad_25 * 0.25m) * ((sueldo_base / 30) / 8), 2);
                    decimal hrs_valor_50 = Math.Round((hrs_cantidad_50 * 1.50m) * ((sueldo_base / 30) / 8), 2);
                    decimal hrs_valor_100 = Math.Round((hrs_cantidad_100 * 2m) * ((sueldo_base / 30) / 8), 2);
                    decimal hrs_valor_total = Math.Round(Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "S", row)), 2);
                    //
                    xmlDocument.Append("<documento>");
                    xmlDocument.Append($"<informacion><cedula>{DocumentModule.CellValue(spreadsheetDocument, sheetName, "BQ", row)}</cedula>" +
                    $"<sueldo_base>{sueldo_base.ToString("0.00")}</sueldo_base>" +
                    $"<dias_base>{dias_base}</dias_base>" +
                    $"<cargo>{DocumentModule.CellValue(spreadsheetDocument, sheetName, "G", row)}</cargo>" +
                    $"<departamento>{DocumentModule.CellValue(spreadsheetDocument, sheetName, "C", row)}</departamento>" +
                    $"<total_ingresos>{Math.Round(Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "AE", row)), 2).ToString("0.00")}</total_ingresos>" +
                    $"<total_descuentos>{Math.Round(Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "AT", row)), 2).ToString("0.00")}</total_descuentos>" +
                    $"<total_a_recibir>{Math.Round(Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, "AU", row)), 2).ToString("0.00")}</total_a_recibir>" +
                    "</informacion>");
                    //ingresos
                    xmlDocument.Append("<ingresos>");
                    xmlDocument.Append($"<rubro><nombre>Sueldo</nombre><valor>{sueldo.ToString("0.00")}</valor></rubro>");
                    //-----------------------------------------------------------------------------
                    if (hrs_valor_100 > 0)
                    {
                        xmlDocument.Append($"<rubro><nombre>{hrs_cantidad_100.ToString("0.00")} horas extraordinarias 100%</nombre><valor>{hrs_valor_100}</valor></rubro>");
                    }
                    if (hrs_valor_50 > 0)
                    {
                        xmlDocument.Append($"<rubro><nombre>{hrs_cantidad_50.ToString("0.00")} horas suplementarias 25%</nombre><valor>{hrs_valor_50}</valor></rubro>");
                    }
                    if (hrs_valor_25 > 0)
                    {
                        xmlDocument.Append($"<rubro><nombre>{hrs_cantidad_25.ToString("0.00")} horas recarga noctura 25%</nombre><valor>{hrs_valor_25}</valor></rubro>");
                    }
                    if (hrs_valor_total > 0)
                    {
                        xmlDocument.Append($"<rubro><nombre>Total horas extras</nombre><valor>{hrs_valor_total.ToString("0.00")}</valor></rubro>");
                    }

                    //--------------------------------------
                    for (int i = 0; i < ingresos.Length; i++)
                    {
                        decimal valor = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, ingresos[i], row));
                        if (valor > 0)
                        {
                            xmlDocument.Append($"<rubro><nombre>{ingresosTitle[i]}</nombre><valor>{valor.ToString("0.00")}</valor></rubro>");
                        }
                    }
                    xmlDocument.Append("</ingresos>");

                    xmlDocument.Append("<descuentos>");
                    for (int i = 0; i < descuentos.Length; i++)
                    {
                        decimal valor = Convert.ToDecimal(DocumentModule.CellValue(spreadsheetDocument, sheetName, descuentos[i], row));
                        if (valor > 0)
                        {
                            xmlDocument.Append($"<rubro><nombre>{descuentosTitle[i]}</nombre><valor>{valor.ToString("0.00")}</valor></rubro>");
                        }
                    }
                    xmlDocument.Append("</descuentos></documento>");
                }
                xmlDocument.Append("</documentos>");
                return xmlDocument.ToString();
                /*return new HttpResponseMessage()
                {
                    Content = new StringContent(xmlDocument.ToString(), Encoding.UTF8, "application/xml")
                };*/

            }

        }


        [HttpPost]
        [Route("pdf-generate")]
        public async Task<DataModel<string>> PdfGenerate()
        {
            DataModel<string> data = new DataModel<string>();
            try
            {


                //todo lo recibido
                System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
                //obtenemos datos campos de form text
                System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
                //obtenemos coleccion de archivos
                HttpFileCollection fileData = httpRequest.Files;

                if (fileData.Count > 0)
                {
                    //file
                    HttpPostedFile file = fileData["DocumentFile"];
                    if (file != null)
                    {
                        if (file.ContentLength <= 5242880)//5mg 5242880 | 2mg 2097152
                        {
                            string sss = file.ContentType.ToString();
                            if (file.ContentType == "application/xml" || file.ContentType == "text/xml")//application/pdf
                            {
                                //
                                if (formData.Count > 0)
                                {
                                    int companyID = Convert.ToInt32(formData["CompanyID"]);
                                    string documentGroupType = formData["DocumentType"].ToString();
                                    string documentGroupDescription = formData["DocumentGroupDescription"].ToString();
                                    DateTime documentGroupDate = Convert.ToDateTime(formData["DocumentGroupDate"]);

                                    switch (documentGroupType)
                                    {
                                        case "CERT-619":
                                            //xml = await XmlGenerateRolPago(file.InputStream);
                                            data.Data = await PdfGenerateRolPago(file.InputStream, companyID, documentGroupType, documentGroupDescription, documentGroupDate);
                                            data.State = data.Data.Count() > 0 ? DataState.error : DataState.ok;
                                            data.Message = data.Data.Count() > 0 ? "Archivo inválido" : "Archivo generado con éxito";
                                            break;
                                        //case "CERT-410":
                                        //XmlGenerateRolPago(file.InputStream);
                                        //break;
                                        default:
                                            throw new Exception("No especifica el tipo de certificado a generar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("No existen datos de texto");
                                }
                            }
                            else
                            {
                                throw new Exception("Archivo: no es un xml ");
                            }
                        }
                        else
                        {
                            throw new Exception("Archivo: excede tamaño límite");
                        }
                    }
                    else
                    {
                        throw new Exception("Arhivo: no existe");
                    }
                }
                else
                {
                    throw new Exception("archivos de imágenes no existen");
                }
            }
            catch (Exception e)
            {
                data.State = DataState.error;
                data.Message = e.Message;
            }

            return data;

        }


        private async Task<List<string>> PdfGenerateRolPago(Stream stream, int companyID, string documentGroupType, string documentGroupDescription, DateTime documentGroupDate)
        {
            //leer archivo xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            //validar documento xml
            List<string> log = await new DocumentModule().PdfValidarRolPago(xmlDoc, companyID);
            if (log.Count > 0)
            {
                return log;
            }
            //raiz documentos
            XmlNode root = xmlDoc.DocumentElement;
            //documento todos
            XmlNodeList documentos = root.ChildNodes;
            //obtenemos una lista de todos los empleados con sus datos para el recibo
            IEnumerable<EmployeeRolPagoModel> employees = await new EmployeeModule().EmployeeRolPagoRead(companyID);
            //fecha generacion
            DateTime documentDateCreation = DateTime.Now;
            //insertar documento group
            int documentGroupID = await new DocumentGroupModule().CreateID(new DocumentGroupModel()
            {
                CompanyID = companyID,
                DocumentGroupActive = true,
                DocumentGroupDate = documentGroupDate,//anio-mes + (1) siempre es dia 1
                DocumentGroupDescription = documentGroupDescription,
                DocumentGroupType = documentGroupType
            });
            //INSERCIONES A TABLA DOCUMENT POR LOTES XML 
            StringBuilder xmlDocumentInsert = new StringBuilder();
            //esto le agregue esperar funciones
            xmlDocumentInsert.Append("<?xml version=\"1.0\" standalone=\"yes\"?><documentos>");

            /*
             @DocumentGroupID int, GENERAL
            @DocumentType varchar(200), GENERAL
            @PersonID varchar(10),-> LOOP CEDULA PERSONAL
            @DocumentXmlID int, VACIO GENERAL
            @DocumentEmailSend varchar(200), LOOP CORREO PERSONAL
            @DocumentEmailSendState tinyint, FALSE
            @DocumentDateEmailSend datetime, -- NULL
            @DocumentDateCreation datetime, GENERAL GENERACION
            @DocumentActive tinyint -- TRUE
             
             */
            //recorremos los documentos
            //VALOR ALEATORIO PARA ID DOCUMENT CODE
            int ramdomDocumentCode = new Random().Next(400, 700);
            ; for (int item = 0; item < documentos.Count; item++)
            {
                //OBTENEMOOS PRIMER REGISTRO-------------------
                string cedula = documentos[item].ChildNodes[0].ChildNodes[0].InnerText;
                EmployeeRolPagoModel employee = employees.Where(x => x.PersonID == cedula).First();




                //
                //parametros momentaneos
                string cabeceraEmpresa = employee.CompanyName;
                string cabeceraDescripcion = documentGroupDescription;
                //fuentes size
                int fontSmall = 8;
                int fontMedium = 10;
                int fontBig = 12;
                //fuentes familia
                string fontFamilly = "Arial";
                //ancho de image
                //float imgQRWidth = 120f;//100f
                //float barraWidth = 156f;
                float tablaBorderWidth = 0.5f;
                BaseColor borderColor = BaseColor.GRAY;
                //
                float tablaBorderWidthBottom = 0f;
                string path = HttpContext.Current.Server.MapPath("~/");
                string pathEmpresa = employee.CompanyPhoto;
                string pathFoto = employee.PersonPhoto;
                string pathFirma = employee.PersonSignatureImage;
                string pathCodeQr = employee.CompanyCodeQrVerification;
                //
                string documentCode = $"{documentGroupType}.{documentGroupID}_{ramdomDocumentCode + item}";//"CERT-269.1_175";
                /*Creating iTextSharp’s Document & Writer*/

                //crear un docuento pdr
                //dimensiones
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                //HttpContext.Current.Response.OutputStream
                //creamos objeto
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
                //lectura
                pdfDoc.Open();

                /*--------------------------------------------------------------------------------
                TABLA PRINCIPAL
                --------------------------------------------------------------------------------*/
                PdfPTable t = new PdfPTable(1);
                //alineamos tabla en el centro
                t.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                t.TotalWidth = 500f;
                t.LockedWidth = true;

                /*--------------------------------------------------------------------------------
               TABLA CABECERA
               --------------------------------------------------------------------------------*/
                PdfPTable tblCabecera = new PdfPTable(2);
                float[] widthsCabecera = new float[] { 200f, 50f };
                tblCabecera.SetWidths(widthsCabecera);
                //alineamos tabla en el centro
                tblCabecera.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblCabecera.TotalWidth = 500f;
                tblCabecera.LockedWidth = true;

                /*--------------------------------------------------------------------------------
                CABECERA - EMPRESA
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCabeceraEmpresa = new PdfPCell();
                cellCabeceraEmpresa.Padding = 0;
                cellCabeceraEmpresa.BorderWidth = 0;
                cellCabeceraEmpresa.BackgroundColor = BaseColor.WHITE;
                cellCabeceraEmpresa.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabeceraEmpresa.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCabeceraEmpresa = new Paragraph(new Chunk(cabeceraEmpresa, FontFactory.GetFont(fontFamilly, fontBig, Font.BOLD, BaseColor.BLACK)));
                prgCabeceraEmpresa.Alignment = 1;
                //agregamos parrafo
                cellCabeceraEmpresa.AddElement(prgCabeceraEmpresa);
                //agregamos celda
                tblCabecera.AddCell(cellCabeceraEmpresa);
                /*--------------------------------------------------------------------------------
                CABHECERA - FOTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCabeceraLogo = new PdfPCell();
                cellCabeceraLogo.Padding = 0;
                cellCabeceraLogo.Rowspan = 3;
                cellCabeceraLogo.BorderWidth = 0;
                cellCabeceraLogo.BackgroundColor = BaseColor.WHITE;
                cellCabeceraLogo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabeceraLogo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //logo empresa
                iTextSharp.text.Image imgEmpresa = iTextSharp.text.Image.GetInstance($"{path}/{pathEmpresa}");
                imgEmpresa.ScaleAbsoluteWidth(100f);
                //agregamos parrafo
                cellCabeceraLogo.AddElement(imgEmpresa);
                //agregamos celda
                tblCabecera.AddCell(cellCabeceraLogo);
                /*--------------------------------------------------------------------------------
                CABECERA - ROL DE PAGO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCabeceraRolPago = new PdfPCell();
                cellCabeceraRolPago.Padding = 0;
                cellCabeceraRolPago.BorderWidth = 0;
                cellCabeceraRolPago.BackgroundColor = BaseColor.WHITE;
                cellCabeceraRolPago.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabeceraRolPago.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCabeceraRolPago = new Paragraph(new Chunk("ROL DE PAGO", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCabeceraRolPago.Alignment = 1;
                //agregamos parrafo
                cellCabeceraRolPago.AddElement(prgCabeceraRolPago);
                //agregamos celda
                tblCabecera.AddCell(cellCabeceraRolPago);

                /*--------------------------------------------------------------------------------
                CABECERA - DESCRIPCION
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCabeceraDescripcion = new PdfPCell();
                cellCabeceraDescripcion.Padding = 0;
                cellCabeceraDescripcion.BorderWidth = 0;
                cellCabeceraDescripcion.BackgroundColor = BaseColor.WHITE;
                cellCabeceraDescripcion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabeceraDescripcion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCabeceraDescripcion = new Paragraph(new Chunk(cabeceraDescripcion, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCabeceraDescripcion.Alignment = 1;
                //agregamos parrafo
                cellCabeceraDescripcion.AddElement(prgCabeceraDescripcion);
                //agregamos celda
                tblCabecera.AddCell(cellCabeceraDescripcion);

                /*--------------------------------------------------------------------------------
               CABECERA - CELDA PARA AGREGAR
               --------------------------------------------------------------------------------*/
                PdfPCell cellCabecera = new PdfPCell();
                cellCabecera.Padding = 0;
                cellCabecera.BorderWidth = 0;
                cellCabecera.BackgroundColor = BaseColor.WHITE;
                cellCabecera.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabecera.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellCabecera.AddElement(tblCabecera);
                t.AddCell(cellCabecera);

                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************


                /*--------------------------------------------------------------------------------
                TABLA Datos
                --------------------------------------------------------------------------------*/
                PdfPTable tblDatos = new PdfPTable(4);
                //alineamos tabla en el centro
                tblDatos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblDatos.TotalWidth = 500f;
                tblDatos.LockedWidth = true;
                /*--------------------------------------------------------------------------------
                Datos - CEDULA
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosCedula = new PdfPCell();
                cellDatosCedula.Padding = 0;
                cellDatosCedula.BorderWidth = 0;
                cellDatosCedula.BackgroundColor = BaseColor.WHITE;
                cellDatosCedula.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosCedula.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosCedula = new Paragraph(new Chunk("CEDULA:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosCedula.Alignment = 0;
                //agregamos parrafo
                cellDatosCedula.AddElement(prgDatosCedula);
                //agregamos celda
                tblDatos.AddCell(cellDatosCedula);

                /*--------------------------------------------------------------------------------
                Datos - CEDULA VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosCedulaValor = new PdfPCell();
                cellDatosCedulaValor.Padding = 0;
                cellDatosCedulaValor.BorderWidth = 0;
                cellDatosCedulaValor.BackgroundColor = BaseColor.WHITE;
                cellDatosCedulaValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosCedulaValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosCedulaValor = new Paragraph(new Chunk(cedula, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosCedulaValor.Alignment = 0;
                //agregamos parrafo
                cellDatosCedulaValor.AddElement(prgDatosCedulaValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosCedulaValor);
                /*--------------------------------------------------------------------------------
                Datos - DEPARTAMENTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosDepartamento = new PdfPCell();
                cellDatosDepartamento.Padding = 0;
                cellDatosDepartamento.BorderWidth = 0;
                cellDatosDepartamento.BackgroundColor = BaseColor.WHITE;
                cellDatosDepartamento.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosDepartamento.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosDepartamento = new Paragraph(new Chunk("DEPARTAMENTO:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosDepartamento.Alignment = 0;
                //agregamos parrafo
                cellDatosDepartamento.AddElement(prgDatosDepartamento);
                //agregamos celda
                tblDatos.AddCell(cellDatosDepartamento);
                /*--------------------------------------------------------------------------------
                Datos - DEPARTAMENTO VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosDepartamentoValor = new PdfPCell();
                cellDatosDepartamentoValor.Padding = 0;
                cellDatosDepartamentoValor.BorderWidth = 0;
                cellDatosDepartamentoValor.BackgroundColor = BaseColor.WHITE;
                cellDatosDepartamentoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosDepartamentoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string departamento = documentos[item].ChildNodes[0].ChildNodes[4].InnerText;
                Paragraph prgDatosDepartamentoValor = new Paragraph(new Chunk(departamento, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosDepartamentoValor.Alignment = 0;
                //agregamos parrafo
                cellDatosDepartamentoValor.AddElement(prgDatosDepartamentoValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosDepartamentoValor);
                /*--------------------------------------------------------------------------------
                Datos - NOMBRE  
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosNombre = new PdfPCell();
                cellDatosNombre.Padding = 0;
                cellDatosNombre.BorderWidth = 0;
                cellDatosNombre.BackgroundColor = BaseColor.WHITE;
                cellDatosNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosNombre = new Paragraph(new Chunk("NOMBRE:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosNombre.Alignment = 0;
                //agregamos parrafo
                cellDatosNombre.AddElement(prgDatosNombre);
                //agregamos celda
                tblDatos.AddCell(cellDatosNombre);
                /*--------------------------------------------------------------------------------
                Datos - NOMBRE VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosNombreValor = new PdfPCell();
                cellDatosNombreValor.Padding = 0;
                cellDatosNombreValor.BorderWidth = 0;
                cellDatosNombreValor.BackgroundColor = BaseColor.WHITE;
                cellDatosNombreValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosNombreValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosNombreValor = new Paragraph(new Chunk(employee.PersonName + " " + employee.PersonSurname, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosNombreValor.Alignment = 0;
                //agregamos parrafo
                cellDatosNombreValor.AddElement(prgDatosNombreValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosNombreValor);
                /*--------------------------------------------------------------------------------
                Datos - CARGO  
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosCargo = new PdfPCell();
                cellDatosCargo.Padding = 0;
                cellDatosCargo.BorderWidth = 0;
                cellDatosCargo.BackgroundColor = BaseColor.WHITE;
                cellDatosCargo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosCargo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosCargo = new Paragraph(new Chunk("CARGO:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosCargo.Alignment = 0;
                //agregamos parrafo
                cellDatosCargo.AddElement(prgDatosCargo);
                //agregamos celda
                tblDatos.AddCell(cellDatosCargo);
                /*--------------------------------------------------------------------------------
                Datos - CARGO VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosCargoValor = new PdfPCell();
                cellDatosCargoValor.Padding = 0;
                cellDatosCargoValor.BorderWidth = 0;
                cellDatosCargoValor.BackgroundColor = BaseColor.WHITE;
                cellDatosCargoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosCargoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string cargo = documentos[item].ChildNodes[0].ChildNodes[3].InnerText;
                Paragraph prgDatosCargoValor = new Paragraph(new Chunk(cargo, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosCargoValor.Alignment = 0;
                //agregamos parrafo
                cellDatosCargoValor.AddElement(prgDatosCargoValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosCargoValor);
                /*--------------------------------------------------------------------------------
                Datos - SUELDO  BASE
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosSueldo = new PdfPCell();
                cellDatosSueldo.Padding = 0;
                cellDatosSueldo.BorderWidth = 0;
                cellDatosSueldo.BackgroundColor = BaseColor.WHITE;
                cellDatosSueldo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosSueldo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosSueldo = new Paragraph(new Chunk("SUELDO BASE:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosSueldo.Alignment = 0;
                //agregamos parrafo
                cellDatosSueldo.AddElement(prgDatosSueldo);
                //agregamos celda
                tblDatos.AddCell(cellDatosSueldo);
                /*--------------------------------------------------------------------------------
                Datos - SUELDO BASE VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosSueldoValor = new PdfPCell();
                cellDatosSueldoValor.Padding = 0;
                cellDatosSueldoValor.BorderWidth = 0;
                cellDatosSueldoValor.BackgroundColor = BaseColor.WHITE;
                cellDatosSueldoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosSueldoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string sueldo_base = documentos[item].ChildNodes[0].ChildNodes[1].InnerText;
                Paragraph prgDatosSueldoValor = new Paragraph(new Chunk(sueldo_base, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosSueldoValor.Alignment = 0;
                //agregamos parrafo
                cellDatosSueldoValor.AddElement(prgDatosSueldoValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosSueldoValor);
                /*--------------------------------------------------------------------------------
                Datos - FECHA DE INGRESO  
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosFecha = new PdfPCell();
                cellDatosFecha.Padding = 0;
                cellDatosFecha.BorderWidth = 0;
                cellDatosFecha.BackgroundColor = BaseColor.WHITE;
                cellDatosFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosFecha = new Paragraph(new Chunk("FECHA INGRESO:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosFecha.Alignment = 0;
                //agregamos parrafo
                cellDatosFecha.AddElement(prgDatosFecha);
                //agregamos celda
                tblDatos.AddCell(cellDatosFecha);
                /*--------------------------------------------------------------------------------
                Datos - FECHA DE INGRESO VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosFechaValor = new PdfPCell();
                cellDatosFechaValor.Padding = 0;
                cellDatosFechaValor.BorderWidth = 0;
                cellDatosFechaValor.BackgroundColor = BaseColor.WHITE;
                cellDatosFechaValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosFechaValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosFechaValor = new Paragraph(new Chunk(employee.EmployeeDateEntry.ToString("dd-MM-yyyy"), FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosFechaValor.Alignment = 0;
                //agregamos parrafo
                cellDatosFechaValor.AddElement(prgDatosFechaValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosFechaValor);



                /*--------------------------------------------------------------------------------
                Datos - DIAS BASE TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosDiaBaseTexto = new PdfPCell();
                cellDatosDiaBaseTexto.Padding = 0;
                cellDatosDiaBaseTexto.BorderWidth = 0;
                cellDatosDiaBaseTexto.BackgroundColor = BaseColor.WHITE;
                cellDatosDiaBaseTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosDiaBaseTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosDiaBaseTexto = new Paragraph(new Chunk("DIAS BASE.", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosDiaBaseTexto.Alignment = 0;
                //agregamos parrafo
                cellDatosDiaBaseTexto.AddElement(prgDatosDiaBaseTexto);
                //agregamos celda
                tblDatos.AddCell(cellDatosDiaBaseTexto);
                /*--------------------------------------------------------------------------------
                Datos - DIAS BASE VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosDiasBaseValor = new PdfPCell();
                cellDatosDiasBaseValor.Padding = 0;
                cellDatosDiasBaseValor.BorderWidth = 0;
                cellDatosDiasBaseValor.BackgroundColor = BaseColor.WHITE;
                cellDatosDiasBaseValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosDiasBaseValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string dias_base = documentos[item].ChildNodes[0].ChildNodes[2].InnerText;
                Paragraph prgDatosDiasBaseValor = new Paragraph(new Chunk(dias_base, FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosDiasBaseValor.Alignment = 0;
                //agregamos parrafo
                cellDatosDiasBaseValor.AddElement(prgDatosDiasBaseValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosDiasBaseValor);
                /*--------------------------------------------------------------------------------
                Datos - VACIO TEXTO  
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosVacioTexto = new PdfPCell();
                cellDatosVacioTexto.Padding = 0;
                cellDatosVacioTexto.BorderWidth = 0;
                cellDatosVacioTexto.BackgroundColor = BaseColor.WHITE;
                cellDatosVacioTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosVacioTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosVacioTexto = new Paragraph(new Chunk("", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgDatosVacioTexto.Alignment = 0;
                //agregamos parrafo
                cellDatosVacioTexto.AddElement(prgDatosVacioTexto);
                //agregamos celda
                tblDatos.AddCell(cellDatosVacioTexto);
                /*--------------------------------------------------------------------------------
                Datos - VACIO VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellDatosVacioValor = new PdfPCell();
                cellDatosVacioValor.Padding = 0;
                cellDatosVacioValor.BorderWidth = 0;
                cellDatosVacioValor.BackgroundColor = BaseColor.WHITE;
                cellDatosVacioValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatosVacioValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgDatosVacioValor = new Paragraph(new Chunk("", FontFactory.GetFont(fontFamilly, fontMedium, Font.NORMAL, BaseColor.BLACK)));
                prgDatosVacioValor.Alignment = 0;
                //agregamos parrafo
                cellDatosVacioValor.AddElement(prgDatosVacioValor);
                //agregamos celda
                tblDatos.AddCell(cellDatosVacioValor);

                /*--------------------------------------------------------------------------------
                Datos - CELDA PARA AGREGAR
                --------------------------------------------------------------------------------*/
                PdfPCell cellDatos = new PdfPCell();
                cellDatos.Padding = 0;
                cellDatos.PaddingTop = 40;
                cellDatos.PaddingBottom = 10;
                cellDatos.BorderWidth = 0;
                cellDatos.BackgroundColor = BaseColor.WHITE;
                cellDatos.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatos.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellDatos.AddElement(tblDatos);
                t.AddCell(cellDatos);

                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************

                /*--------------------------------------------------------------------------------
                TABLA CUERPO (contendra a ingresos y descuentos)
                --------------------------------------------------------------------------------*/
                PdfPTable tblCuerpo = new PdfPTable(2);
                //alineamos tabla en el centro
                tblCuerpo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblCuerpo.TotalWidth = 500f;
                tblCuerpo.LockedWidth = true;
                /*--------------------------------------------------------------------------------
                TABLA NGRESOS
                --------------------------------------------------------------------------------*/
                PdfPTable tblIngresos = new PdfPTable(2);
                float[] widths = new float[] { 200f, 50f };
                tblIngresos.SetWidths(widths);
                //alineamos tabla en el centro
                tblIngresos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblIngresos.TotalWidth = 250f;
                tblIngresos.LockedWidth = true;
                /*--------------------------------------------------------------------------------
                CUERPO - ingresos texto (ocupa 2 columnas)
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoTexto = new PdfPCell();
                cellCuerpoIngresoTexto.PaddingBottom = 5;
                cellCuerpoIngresoTexto.BorderWidth = 1;
                cellCuerpoIngresoTexto.Colspan = 2;
                cellCuerpoIngresoTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoIngresoTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoTexto = new Paragraph(new Chunk("INGRESOS", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoTexto.Alignment = 1;
                //agregamos parrafo
                cellCuerpoIngresoTexto.AddElement(prgCuerpoIngresoTexto);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoTexto);
                /*--------------------------------------------------------------------------------
                CUERPO - ingreso concepto texto
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoConceptoTexto = new PdfPCell();
                cellCuerpoIngresoConceptoTexto.PaddingBottom = 5;
                cellCuerpoIngresoConceptoTexto.PaddingLeft = 5;
                cellCuerpoIngresoConceptoTexto.BorderWidth = 1;
                cellCuerpoIngresoConceptoTexto.BorderWidthRight = 0;
                cellCuerpoIngresoConceptoTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoIngresoConceptoTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoConceptoTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoConceptoTexto = new Paragraph(new Chunk("CONCEPTO", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoConceptoTexto.Alignment = 0;
                //agregamos parrafo
                cellCuerpoIngresoConceptoTexto.AddElement(prgCuerpoIngresoConceptoTexto);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoConceptoTexto);
                /*--------------------------------------------------------------------------------
               CUERPO ingreso- valor texto
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoValorTexto = new PdfPCell();
                cellCuerpoIngresoValorTexto.PaddingBottom = 5;
                cellCuerpoIngresoValorTexto.PaddingRight = 5;
                cellCuerpoIngresoValorTexto.BorderWidth = 1;
                cellCuerpoIngresoValorTexto.BorderWidthLeft = 0;
                cellCuerpoIngresoValorTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoIngresoValorTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoValorTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoValorTexto = new Paragraph(new Chunk("VALOR", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoValorTexto.Alignment = 2;
                //agregamos parrafo
                cellCuerpoIngresoValorTexto.AddElement(prgCuerpoIngresoValorTexto);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoValorTexto);
                /*--------------------------------------------------------------------------------
                CUERPO - ingreso concepto valor
                --------------------------------------------------------------------------------*/
                int MaxRubros = 15;//cantidad minima de registros
                int totalRubrosIngresosExistentes = documentos[item].ChildNodes[1].ChildNodes.Count;//5
                //recorremos los rubros existentente en ingresos
                for (int bb = 0; bb < totalRubrosIngresosExistentes; bb++)
                {
                    //creamos celda
                    PdfPCell cellCuerpoIngresoConceptoValor = new PdfPCell();
                    cellCuerpoIngresoConceptoValor.Padding = 0;
                    cellCuerpoIngresoConceptoValor.PaddingLeft = 5;
                    cellCuerpoIngresoConceptoValor.BorderWidth = 0;
                    cellCuerpoIngresoConceptoValor.BorderWidthLeft = 1;
                    cellCuerpoIngresoConceptoValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoIngresoConceptoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoIngresoConceptoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    //nombre rubro
                    string name = documentos[item].ChildNodes[1].ChildNodes[bb].ChildNodes[0].InnerText;
                    Paragraph prgCuerpoIngresoConceptoValor = new Paragraph(new Chunk(name, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                    prgCuerpoIngresoConceptoValor.Alignment = 0;
                    //agregamos parrafo
                    cellCuerpoIngresoConceptoValor.AddElement(prgCuerpoIngresoConceptoValor);
                    //agregamos celda
                    tblIngresos.AddCell(cellCuerpoIngresoConceptoValor);
                    /*--------------------------------------------------------------------------------
                   CUERPO ingreso- valor valor
                   --------------------------------------------------------------------------------*/
                    //creamos celda
                    PdfPCell cellCuerpoIngresoValorValor = new PdfPCell();
                    cellCuerpoIngresoValorValor.Padding = 0;
                    cellCuerpoIngresoValorValor.PaddingRight = 5;
                    cellCuerpoIngresoValorValor.PaddingBottom = 5;
                    cellCuerpoIngresoValorValor.BorderWidth = 0;
                    cellCuerpoIngresoValorValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoIngresoValorValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoIngresoValorValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    //valor rubro
                    string valor = documentos[item].ChildNodes[1].ChildNodes[bb].ChildNodes[1].InnerText;
                    Paragraph prgCuerpoIngresoValorValor = new Paragraph(new Chunk(valor, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                    prgCuerpoIngresoValorValor.Alignment = 2;
                    //agregamos parrafo
                    cellCuerpoIngresoValorValor.AddElement(prgCuerpoIngresoValorValor);
                    //agregamos celda
                    tblIngresos.AddCell(cellCuerpoIngresoValorValor);
                }

                //--------------------------------------------------------------------------------

                //cramos rubros vacios para completar alto minimo de rubros
                int totalRubrosIngresosBlancos = (totalRubrosIngresosExistentes >= MaxRubros) ? 0 : MaxRubros - totalRubrosIngresosExistentes;
                for (int i = 0; i < totalRubrosIngresosBlancos; i++)
                {

                    //creamos celda
                    PdfPCell cellCuerpoIngresoConceptoValor = new PdfPCell();
                    cellCuerpoIngresoConceptoValor.Padding = 0;
                    cellCuerpoIngresoConceptoValor.PaddingLeft = 5;
                    cellCuerpoIngresoConceptoValor.BorderWidth = 0;
                    cellCuerpoIngresoConceptoValor.BorderWidthLeft = 1;
                    cellCuerpoIngresoConceptoValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoIngresoConceptoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoIngresoConceptoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    Paragraph prgCuerpoIngresoConceptoValor = new Paragraph(new Chunk("-", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.WHITE)));
                    prgCuerpoIngresoConceptoValor.Alignment = 0;
                    //agregamos parrafo
                    cellCuerpoIngresoConceptoValor.AddElement(prgCuerpoIngresoConceptoValor);
                    //agregamos celda
                    tblIngresos.AddCell(cellCuerpoIngresoConceptoValor);
                    /*--------------------------------------------------------------------------------
                   CUERPO ingreso- valor valor
                   --------------------------------------------------------------------------------*/
                    //creamos celda
                    PdfPCell cellCuerpoIngresoValorValor = new PdfPCell();
                    cellCuerpoIngresoValorValor.Padding = 0;
                    cellCuerpoIngresoValorValor.PaddingRight = 5;
                    cellCuerpoIngresoValorValor.PaddingBottom = 5;
                    cellCuerpoIngresoValorValor.BorderWidth = 0;
                    cellCuerpoIngresoValorValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoIngresoValorValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoIngresoValorValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    Paragraph prgCuerpoIngresoValorValor = new Paragraph(new Chunk(".", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.WHITE)));
                    prgCuerpoIngresoValorValor.Alignment = 2;
                    //agregamos parrafo
                    cellCuerpoIngresoValorValor.AddElement(prgCuerpoIngresoValorValor);
                    //agregamos celda
                    tblIngresos.AddCell(cellCuerpoIngresoValorValor);
                }

                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************


                /*--------------------------------------------------------------------------------
                CUERPO - INGRESO TOTAL TEXTO 
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoConceptoValorTotal = new PdfPCell();
                cellCuerpoIngresoConceptoValorTotal.Padding = 0;
                cellCuerpoIngresoConceptoValorTotal.PaddingLeft = 5;
                cellCuerpoIngresoConceptoValorTotal.BorderWidth = 0;
                cellCuerpoIngresoConceptoValorTotal.BorderWidthLeft = 1;
                cellCuerpoIngresoConceptoValorTotal.BorderWidthTop = 1;
                cellCuerpoIngresoConceptoValorTotal.BackgroundColor = BaseColor.WHITE;
                cellCuerpoIngresoConceptoValorTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoConceptoValorTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoConceptoValorTotal = new Paragraph(new Chunk("TOTAL INGRESOS", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoConceptoValorTotal.Alignment = 0;
                //agregamos parrafo
                cellCuerpoIngresoConceptoValorTotal.AddElement(prgCuerpoIngresoConceptoValorTotal);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoConceptoValorTotal);
                /*--------------------------------------------------------------------------------
               CUERPO -  INGRESO TOTAL VALOR
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoValorValorTotal = new PdfPCell();
                cellCuerpoIngresoValorValorTotal.Padding = 0;
                cellCuerpoIngresoValorValorTotal.PaddingRight = 5;
                cellCuerpoIngresoValorValorTotal.PaddingBottom = 5;
                cellCuerpoIngresoValorValorTotal.BorderWidth = 0;
                cellCuerpoIngresoValorValorTotal.BorderWidthTop = 1;
                cellCuerpoIngresoValorValorTotal.BackgroundColor = BaseColor.WHITE;
                cellCuerpoIngresoValorValorTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoValorValorTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string total_ingresos = documentos[item].ChildNodes[0].ChildNodes[5].InnerText;
                Paragraph prgCuerpoIngresoValorValorTotal = new Paragraph(new Chunk(total_ingresos, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoValorValorTotal.Alignment = 2;
                //agregamos parrafo
                cellCuerpoIngresoValorValorTotal.AddElement(prgCuerpoIngresoValorValorTotal);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoValorValorTotal);


                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************


                /*--------------------------------------------------------------------------------
                CUERPO - INGRESO TOTAL VACIO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoConceptoValorTotalVacio = new PdfPCell();
                cellCuerpoIngresoConceptoValorTotalVacio.Padding = 0;
                cellCuerpoIngresoConceptoValorTotalVacio.BorderWidth = 0;
                cellCuerpoIngresoConceptoValorTotalVacio.BorderWidthBottom = 1;
                cellCuerpoIngresoConceptoValorTotalVacio.BackgroundColor = BaseColor.WHITE;
                cellCuerpoIngresoConceptoValorTotalVacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoConceptoValorTotalVacio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoConceptoValorTotalVacio = new Paragraph(new Chunk("", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoConceptoValorTotalVacio.Alignment = 0;
                //agregamos parrafo
                cellCuerpoIngresoConceptoValorTotalVacio.AddElement(prgCuerpoIngresoConceptoValorTotalVacio);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoConceptoValorTotalVacio);
                /*--------------------------------------------------------------------------------
               CUERPO -  INGRESO TOTAL VALOR VACIO
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoIngresoValorValorTotalVacio = new PdfPCell();
                cellCuerpoIngresoValorValorTotalVacio.Padding = 0;
                cellCuerpoIngresoValorValorTotalVacio.BorderWidth = 0;
                cellCuerpoIngresoValorValorTotalVacio.BorderWidthBottom = 1;
                cellCuerpoIngresoValorValorTotalVacio.BackgroundColor = BaseColor.WHITE;
                cellCuerpoIngresoValorValorTotalVacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoIngresoValorValorTotalVacio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoIngresoValorValorTotalVacio = new Paragraph(new Chunk("", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoIngresoValorValorTotalVacio.Alignment = 2;
                //agregamos parrafo
                cellCuerpoIngresoValorValorTotalVacio.AddElement(prgCuerpoIngresoValorValorTotalVacio);
                //agregamos celda
                tblIngresos.AddCell(cellCuerpoIngresoValorValorTotalVacio);



                /*--------------------------------------------------------------------------------
                   ingresos - CELDA PARA AGREGAR
                   --------------------------------------------------------------------------------*/
                PdfPCell cellIngreso = new PdfPCell();
                cellIngreso.Padding = 0;
                cellIngreso.PaddingTop = 0;
                cellIngreso.PaddingBottom = 0;
                cellIngreso.BorderWidth = 1;
                cellIngreso.BorderWidthBottom = 0;
                cellIngreso.BorderWidthLeft = 0;
                cellIngreso.BackgroundColor = BaseColor.WHITE;
                cellIngreso.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellIngreso.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellIngreso.AddElement(tblIngresos);

                //agregamos al cuerpo la celda que contiene celda ingreso
                tblCuerpo.AddCell(cellIngreso);


                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************



                /*--------------------------------------------------------------------------------
               TABLA DESCUENTOS
               --------------------------------------------------------------------------------*/
                PdfPTable tblDescuentos = new PdfPTable(2);
                float[] widthsDescuentos = new float[] { 200f, 50f };
                tblDescuentos.SetWidths(widthsDescuentos);
                //alineamos tabla en el centro
                tblDescuentos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblDescuentos.TotalWidth = 250f;
                tblDescuentos.LockedWidth = true;
                /*--------------------------------------------------------------------------------
                CUERPO - descuentos texto (ocupa 2 columnas)
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoDescuentoTexto = new PdfPCell();
                cellCuerpoDescuentoTexto.PaddingBottom = 5;
                cellCuerpoDescuentoTexto.BorderWidth = 1;
                cellCuerpoDescuentoTexto.Colspan = 2;
                cellCuerpoDescuentoTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoDescuentoTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoDescuentoTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoDescuentoTexto = new Paragraph(new Chunk("DESCUENTOS", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoDescuentoTexto.Alignment = 1;
                //agregamos parrafo
                cellCuerpoDescuentoTexto.AddElement(prgCuerpoDescuentoTexto);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoDescuentoTexto);
                /*--------------------------------------------------------------------------------
                CUERPO - descuentos concepto texto
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoDescuentoConceptoTexto = new PdfPCell();
                cellCuerpoDescuentoConceptoTexto.PaddingBottom = 5;
                cellCuerpoDescuentoConceptoTexto.PaddingLeft = 5;
                cellCuerpoDescuentoConceptoTexto.BorderWidth = 1;
                cellCuerpoDescuentoConceptoTexto.BorderWidthRight = 0;
                cellCuerpoDescuentoConceptoTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoDescuentoConceptoTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoDescuentoConceptoTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoDescuentoConceptoTexto = new Paragraph(new Chunk("CONCEPTO", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoDescuentoConceptoTexto.Alignment = 0;
                //agregamos parrafo
                cellCuerpoDescuentoConceptoTexto.AddElement(prgCuerpoDescuentoConceptoTexto);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoDescuentoConceptoTexto);
                /*--------------------------------------------------------------------------------
               CUERPO descuento- valor texto
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoDescuentoValorTexto = new PdfPCell();
                cellCuerpoDescuentoValorTexto.PaddingBottom = 5;
                cellCuerpoDescuentoValorTexto.PaddingRight = 5;
                cellCuerpoDescuentoValorTexto.BorderWidth = 1;
                cellCuerpoDescuentoValorTexto.BorderWidthLeft = 0;
                cellCuerpoDescuentoValorTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoDescuentoValorTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoDescuentoValorTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoDescuentoValorTexto = new Paragraph(new Chunk("VALOR", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoDescuentoValorTexto.Alignment = 2;
                //agregamos parrafo
                cellCuerpoDescuentoValorTexto.AddElement(prgCuerpoDescuentoValorTexto);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoDescuentoValorTexto);
                /*--------------------------------------------------------------------------------
               CUERPO - ingreso concepto valor
               --------------------------------------------------------------------------------*/
                //int MaxRubros = 15;//cantidad minima de registros
                int totalRubrosDescuentosExistentes = documentos[item].ChildNodes[2].ChildNodes.Count;
                //RUBROS DESCUENTOS
                for (int bb = 0; bb < totalRubrosDescuentosExistentes; bb++)
                {
                    //creamos celda
                    PdfPCell cellCuerpoDescuentoConceptoValor = new PdfPCell();
                    cellCuerpoDescuentoConceptoValor.Padding = 0;
                    cellCuerpoDescuentoConceptoValor.PaddingLeft = 5;
                    cellCuerpoDescuentoConceptoValor.BorderWidth = 0;
                    cellCuerpoDescuentoConceptoValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoDescuentoConceptoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoDescuentoConceptoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    string name = documentos[item].ChildNodes[2].ChildNodes[bb].ChildNodes[0].InnerText;
                    Paragraph prgCuerpoDescuentoConceptoValor = new Paragraph(new Chunk(name, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                    prgCuerpoDescuentoConceptoValor.Alignment = 0;
                    //agregamos parrafo
                    cellCuerpoDescuentoConceptoValor.AddElement(prgCuerpoDescuentoConceptoValor);
                    //agregamos celda
                    tblDescuentos.AddCell(cellCuerpoDescuentoConceptoValor);
                    /*--------------------------------------------------------------------------------
                   CUERPO Descuento- valor valor
                   --------------------------------------------------------------------------------*/
                    //creamos celda
                    PdfPCell cellCuerpoDescuentoValorValor = new PdfPCell();
                    cellCuerpoDescuentoValorValor.Padding = 0;
                    cellCuerpoDescuentoValorValor.PaddingRight = 5;
                    cellCuerpoDescuentoValorValor.PaddingBottom = 5;
                    cellCuerpoDescuentoValorValor.BorderWidth = 0;
                    cellCuerpoDescuentoValorValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoDescuentoValorValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoDescuentoValorValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    string valor = documentos[item].ChildNodes[2].ChildNodes[bb].ChildNodes[1].InnerText;
                    Paragraph prgCuerpoDescuentoValorValor = new Paragraph(new Chunk(valor, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                    prgCuerpoDescuentoValorValor.Alignment = 2;
                    //agregamos parrafo
                    cellCuerpoDescuentoValorValor.AddElement(prgCuerpoDescuentoValorValor);
                    //agregamos celda
                    tblDescuentos.AddCell(cellCuerpoDescuentoValorValor);
                }


                int totalRubrosDescuentosBlancos = (totalRubrosDescuentosExistentes >= MaxRubros) ? 0 : MaxRubros - totalRubrosDescuentosExistentes;
                for (int i = 0; i < totalRubrosDescuentosBlancos; i++)
                {

                    //creamos celda
                    PdfPCell cellCuerpoDescuentoConceptoValor = new PdfPCell();
                    cellCuerpoDescuentoConceptoValor.Padding = 0;
                    cellCuerpoDescuentoConceptoValor.PaddingLeft = 5;
                    cellCuerpoDescuentoConceptoValor.BorderWidth = 0;
                    cellCuerpoDescuentoConceptoValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoDescuentoConceptoValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoDescuentoConceptoValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    Paragraph prgCuerpoDescuentoConceptoValor = new Paragraph(new Chunk("---", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.WHITE)));
                    prgCuerpoDescuentoConceptoValor.Alignment = 0;
                    //agregamos parrafo
                    cellCuerpoDescuentoConceptoValor.AddElement(prgCuerpoDescuentoConceptoValor);
                    //agregamos celda
                    tblDescuentos.AddCell(cellCuerpoDescuentoConceptoValor);
                    /*--------------------------------------------------------------------------------
                   CUERPO Descuento- valor valor
                   --------------------------------------------------------------------------------*/
                    //creamos celda
                    PdfPCell cellCuerpoDescuentoValorValor = new PdfPCell();
                    cellCuerpoDescuentoValorValor.Padding = 0;
                    cellCuerpoDescuentoValorValor.PaddingRight = 5;
                    cellCuerpoDescuentoValorValor.PaddingBottom = 5;
                    cellCuerpoDescuentoValorValor.BorderWidth = 0;
                    cellCuerpoDescuentoValorValor.BackgroundColor = BaseColor.WHITE;
                    cellCuerpoDescuentoValorValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cellCuerpoDescuentoValorValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //creamos parafo
                    Paragraph prgCuerpoDescuentoValorValor = new Paragraph(new Chunk("...", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.WHITE)));
                    prgCuerpoDescuentoValorValor.Alignment = 2;
                    //agregamos parrafo
                    cellCuerpoDescuentoValorValor.AddElement(prgCuerpoDescuentoValorValor);
                    //agregamos celda
                    tblDescuentos.AddCell(cellCuerpoDescuentoValorValor);
                }








                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************





                /*--------------------------------------------------------------------------------
                CUERPO -  DESCUENTO TOTAL TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoDescuentoConceptoValorTotal = new PdfPCell();
                cellCuerpoDescuentoConceptoValorTotal.Padding = 0;
                cellCuerpoDescuentoConceptoValorTotal.PaddingLeft = 5;
                cellCuerpoDescuentoConceptoValorTotal.BorderWidth = 0;
                cellCuerpoDescuentoConceptoValorTotal.BorderWidthTop = 1;
                cellCuerpoDescuentoConceptoValorTotal.BackgroundColor = BaseColor.WHITE;
                cellCuerpoDescuentoConceptoValorTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoDescuentoConceptoValorTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoDescuentoConceptoValorTotal = new Paragraph(new Chunk("TOTAL DESCUENTOS", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoDescuentoConceptoValorTotal.Alignment = 0;
                //agregamos parrafo
                cellCuerpoDescuentoConceptoValorTotal.AddElement(prgCuerpoDescuentoConceptoValorTotal);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoDescuentoConceptoValorTotal);
                /*--------------------------------------------------------------------------------
               CUERPO -  DESCUENTO TOTAL VALOR
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoDescuentoValorTotalValor = new PdfPCell();
                cellCuerpoDescuentoValorTotalValor.Padding = 0;
                cellCuerpoDescuentoValorTotalValor.PaddingRight = 5;
                cellCuerpoDescuentoValorTotalValor.PaddingBottom = 5;
                cellCuerpoDescuentoValorTotalValor.BorderWidth = 0;
                cellCuerpoDescuentoValorTotalValor.BorderWidthTop = 1;
                cellCuerpoDescuentoValorTotalValor.BackgroundColor = BaseColor.WHITE;
                cellCuerpoDescuentoValorTotalValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoDescuentoValorTotalValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string total_desuentos = documentos[item].ChildNodes[0].ChildNodes[6].InnerText;
                Paragraph prgCuerpoDescuentoValorTotalValor = new Paragraph(new Chunk(total_desuentos, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoDescuentoValorTotalValor.Alignment = 2;
                //agregamos parrafo
                cellCuerpoDescuentoValorTotalValor.AddElement(prgCuerpoDescuentoValorTotalValor);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoDescuentoValorTotalValor);


                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************





                /*--------------------------------------------------------------------------------
                CUERPO -  GRAN TOTAL TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellCuerpoGranTotalTexto = new PdfPCell();
                cellCuerpoGranTotalTexto.Padding = 0;
                cellCuerpoGranTotalTexto.PaddingLeft = 5;
                cellCuerpoGranTotalTexto.BorderWidth = 0;
                cellCuerpoGranTotalTexto.BorderWidthTop = 1;
                cellCuerpoGranTotalTexto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellCuerpoGranTotalTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpoGranTotalTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgCuerpoGranTotalTexto = new Paragraph(new Chunk("TOTAL A RECIBIR", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgCuerpoGranTotalTexto.Alignment = 0;
                //agregamos parrafo
                cellCuerpoGranTotalTexto.AddElement(prgCuerpoGranTotalTexto);
                //agregamos celda
                tblDescuentos.AddCell(cellCuerpoGranTotalTexto);
                /*--------------------------------------------------------------------------------
               CUERPO -  GRAN TOTAL VALOR
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellGranTotalValor = new PdfPCell();
                cellGranTotalValor.Padding = 0;
                cellGranTotalValor.PaddingRight = 5;
                cellGranTotalValor.PaddingBottom = 5;
                cellGranTotalValor.BorderWidth = 0;
                cellGranTotalValor.BorderWidthTop = 1;
                cellGranTotalValor.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellGranTotalValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellGranTotalValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                string gran_total = documentos[item].ChildNodes[0].ChildNodes[7].InnerText;
                Paragraph prgGranTotalValor = new Paragraph(new Chunk(gran_total, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgGranTotalValor.Alignment = 2;
                //agregamos parrafo
                cellGranTotalValor.AddElement(prgGranTotalValor);
                //agregamos celda
                tblDescuentos.AddCell(cellGranTotalValor);



                /*--------------------------------------------------------------------------------
                   descuentos - CELDA PARA AGREGAR
                   --------------------------------------------------------------------------------*/
                PdfPCell cellDescuento = new PdfPCell();
                cellDescuento.Padding = 0;
                cellDescuento.PaddingTop = 0;
                cellDescuento.PaddingBottom = 0;
                cellDescuento.BorderWidth = 1;
                cellDescuento.BackgroundColor = BaseColor.WHITE;
                cellDescuento.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDescuento.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellDescuento.AddElement(tblDescuentos);

                //agregamos al cuerpo la celda que contiene celda Descuento
                tblCuerpo.AddCell(cellDescuento);


                /*--------------------------------------------------------------------------------
                CUERPO - CELDA PARA AGREGAR
                --------------------------------------------------------------------------------*/
                PdfPCell cellCuerpo = new PdfPCell();
                cellCuerpo.Padding = 0;
                cellCuerpo.PaddingTop = 30;
                cellCuerpo.PaddingBottom = 0;
                cellCuerpo.BorderWidth = 0;
                cellCuerpo.BackgroundColor = BaseColor.WHITE;
                cellCuerpo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellCuerpo.AddElement(tblCuerpo);

                //agregamos al cuerpo la celda que contiene ingresos
                t.AddCell(cellCuerpo);



                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************
                //***********************************************************************************



                /*--------------------------------------------------------------------------------
                TABLA PIE
                --------------------------------------------------------------------------------*/
                PdfPTable tblPie = new PdfPTable(4);
                float[] widthsPie = new float[] { 70f, 100f, 230f, 100f };
                tblPie.SetWidths(widthsPie);
                //alineamos tabla en el centro
                tblPie.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //ancho de la tabla
                tblPie.TotalWidth = 500f;
                tblPie.LockedWidth = true;

                /*--------------------------------------------------------------------------------
                PIE - FOTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieFoto = new PdfPCell();
                cellPieFoto.Padding = 5;
                cellPieFoto.Rowspan = 6;
                cellPieFoto.BorderWidth = 1;
                cellPieFoto.BorderColor = BaseColor.LIGHT_GRAY;
                cellPieFoto.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellPieFoto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieFoto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //Foto empresa
                iTextSharp.text.Image imgFoto = iTextSharp.text.Image.GetInstance($"{path}/{pathFoto}");
                //imgFoto.ScaleAbsoluteHeight(100f);
                imgFoto.ScaleAbsoluteWidth(60f);
                //agregamos parrafo
                cellPieFoto.AddElement(imgFoto);
                //agregamos celda
                tblPie.AddCell(cellPieFoto);

                /*--------------------------------------------------------------------------------
                PIE - CODE QR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieCodeQr = new PdfPCell();
                cellPieCodeQr.Padding = 0;
                cellPieCodeQr.Rowspan = 6;
                cellPieCodeQr.BorderWidth = 0;
                cellPieCodeQr.BackgroundColor = BaseColor.WHITE;
                cellPieCodeQr.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieCodeQr.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //CodeQr empresa
                iTextSharp.text.Image imgCodeQr = iTextSharp.text.Image.GetInstance($"{path}/{pathCodeQr}");
                imgCodeQr.ScaleAbsoluteWidth(100f);
                //agregamos parrafo
                cellPieCodeQr.AddElement(imgCodeQr);
                //agregamos celda
                tblPie.AddCell(cellPieCodeQr);

                /*--------------------------------------------------------------------------------
                Pie - FECHA FIRMA TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieFechaFirmaTexto = new PdfPCell();
                cellPieFechaFirmaTexto.Padding = 0;
                cellPieFechaFirmaTexto.BorderWidth = 0;
                cellPieFechaFirmaTexto.BackgroundColor = BaseColor.WHITE;
                cellPieFechaFirmaTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieFechaFirmaTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieFechaFirmaTexto = new Paragraph(new Chunk("Fecha Firma:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgPieFechaFirmaTexto.Alignment = 0;
                //agregamos parrafo
                cellPieFechaFirmaTexto.AddElement(prgPieFechaFirmaTexto);
                //agregamos celda
                tblPie.AddCell(cellPieFechaFirmaTexto);

                /*--------------------------------------------------------------------------------
               PIE - IMAGEN FIRMA
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieFirma = new PdfPCell();
                cellPieFirma.Padding = 0;
                cellPieFirma.PaddingBottom = 5;
                cellPieFirma.Rowspan = 6;
                cellPieFirma.BorderWidth = 0;
                cellPieFirma.BackgroundColor = BaseColor.WHITE;
                cellPieFirma.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieFirma.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                //Firma empresa
                iTextSharp.text.Image imgFirma = iTextSharp.text.Image.GetInstance($"{path}/{pathFirma}");
                imgFirma.ScaleAbsoluteWidth(100f);
                //agregamos parrafo
                cellPieFirma.AddElement(imgFirma);
                //agregamos celda
                tblPie.AddCell(cellPieFirma);

                /*--------------------------------------------------------------------------------
                Pie - FECHA FIRMA VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieFechaFirmaValor = new PdfPCell();
                cellPieFechaFirmaValor.Padding = 0;
                cellPieFechaFirmaValor.BorderWidth = 0;
                cellPieFechaFirmaValor.BackgroundColor = BaseColor.WHITE;
                cellPieFechaFirmaValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieFechaFirmaValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieFechaFirmaValor = new Paragraph(new Chunk(documentDateCreation.ToString("dd-MM-yyyy (HH:mm)"), FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgPieFechaFirmaValor.Alignment = 0;
                //agregamos parrafo
                cellPieFechaFirmaValor.AddElement(prgPieFechaFirmaValor);
                //agregamos celda
                tblPie.AddCell(cellPieFechaFirmaValor);

                /*--------------------------------------------------------------------------------
                Pie - CODIGO DE VERIFICACION TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieCodigoVerificacionTexto = new PdfPCell();
                cellPieCodigoVerificacionTexto.Padding = 0;
                cellPieCodigoVerificacionTexto.BorderWidth = 0;
                cellPieCodigoVerificacionTexto.BackgroundColor = BaseColor.WHITE;
                cellPieCodigoVerificacionTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieCodigoVerificacionTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieCodigoVerificacionTexto = new Paragraph(new Chunk("Código de verificación:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgPieCodigoVerificacionTexto.Alignment = 0;
                //agregamos parrafo
                cellPieCodigoVerificacionTexto.AddElement(prgPieCodigoVerificacionTexto);
                //agregamos celda
                tblPie.AddCell(cellPieCodigoVerificacionTexto);

                /*--------------------------------------------------------------------------------
                Pie - CODIGO DE VERIFICACION VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieCodigoVerificacionValor = new PdfPCell();
                cellPieCodigoVerificacionValor.Padding = 0;
                cellPieCodigoVerificacionValor.BorderWidth = 0;
                cellPieCodigoVerificacionValor.BackgroundColor = BaseColor.WHITE;
                cellPieCodigoVerificacionValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieCodigoVerificacionValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieCodigoVerificacionValor = new Paragraph(new Chunk(documentCode, FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgPieCodigoVerificacionValor.Alignment = 0;
                //agregamos parrafo
                cellPieCodigoVerificacionValor.AddElement(prgPieCodigoVerificacionValor);
                //agregamos celda
                tblPie.AddCell(cellPieCodigoVerificacionValor);

                /*--------------------------------------------------------------------------------
                Pie - URL VERIFICACION TEXTO
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieUrlVerificacionTexto = new PdfPCell();
                cellPieUrlVerificacionTexto.Padding = 0;
                cellPieUrlVerificacionTexto.BorderWidth = 0;
                cellPieUrlVerificacionTexto.BackgroundColor = BaseColor.WHITE;
                cellPieUrlVerificacionTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieUrlVerificacionTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieUrlVerificacionTexto = new Paragraph(new Chunk("Url verificación:", FontFactory.GetFont(fontFamilly, fontMedium, Font.BOLD, BaseColor.BLACK)));
                prgPieUrlVerificacionTexto.Alignment = 0;
                //agregamos parrafo
                cellPieUrlVerificacionTexto.AddElement(prgPieUrlVerificacionTexto);
                //agregamos celda
                tblPie.AddCell(cellPieUrlVerificacionTexto);

                /*--------------------------------------------------------------------------------
                Pie - URL VERIFICACION VALOR
                --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieUrlVerificacionValor = new PdfPCell();
                cellPieUrlVerificacionValor.Padding = 0;
                cellPieUrlVerificacionValor.BorderWidth = 0;
                cellPieUrlVerificacionValor.BackgroundColor = BaseColor.WHITE;
                cellPieUrlVerificacionValor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieUrlVerificacionValor.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Chunk portText = new Chunk($"DOCUMENTOS {employee.CompanyName.Substring(0, 10)}...", FontFactory.GetFont(fontFamilly, fontSmall, Font.UNDERLINE, BaseColor.BLUE));
                portText.SetAnchor(new Uri(employee.CompanyUrlVerification));
                Paragraph prgPieUrlVerificacionValor = new Paragraph();
                prgPieUrlVerificacionValor.Alignment = 0;
                prgPieUrlVerificacionValor.Add(portText);
                //agregamos parrafo
                cellPieUrlVerificacionValor.AddElement(prgPieUrlVerificacionValor);
                //agregamos celda
                tblPie.AddCell(cellPieUrlVerificacionValor);


                /*--------------------------------------------------------------------------------
               Pie - VACIO
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieVacio = new PdfPCell();
                cellPieVacio.Padding = 0;
                cellPieVacio.Colspan = 3;
                cellPieVacio.BorderWidth = 0;
                cellPieVacio.BackgroundColor = BaseColor.WHITE;
                cellPieVacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieVacio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieVacio = new Paragraph(new Chunk("", FontFactory.GetFont(fontFamilly, fontBig, Font.BOLD, BaseColor.BLACK)));
                prgPieVacio.Alignment = 0;
                //agregamos parrafo
                cellPieVacio.AddElement(prgPieVacio);
                //agregamos celda
                tblPie.AddCell(cellPieVacio);


                /*--------------------------------------------------------------------------------
               Pie - RECIBIDO TEXTO
               --------------------------------------------------------------------------------*/
                //creamos celda
                PdfPCell cellPieRecibidoTexto = new PdfPCell();
                cellPieRecibidoTexto.Padding = 0;
                cellPieRecibidoTexto.BorderWidth = 0;
                cellPieRecibidoTexto.BorderWidthTop = 1;
                cellPieRecibidoTexto.BackgroundColor = BaseColor.WHITE;
                cellPieRecibidoTexto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPieRecibidoTexto.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //creamos parafo
                Paragraph prgPieRecibidoTexto = new Paragraph(new Chunk("RECIBIDO", FontFactory.GetFont(fontFamilly, fontBig, Font.BOLD, BaseColor.BLACK)));
                prgPieRecibidoTexto.Alignment = 1;
                //agregamos parrafo
                cellPieRecibidoTexto.AddElement(prgPieRecibidoTexto);
                //agregamos celda
                tblPie.AddCell(cellPieRecibidoTexto);

                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


                /*--------------------------------------------------------------------------------
                PIE - CELDA PARA AGREGAR
                --------------------------------------------------------------------------------*/
                PdfPCell cellPie = new PdfPCell();
                cellPie.Padding = 0;
                cellPie.PaddingTop = 40;
                cellPie.PaddingBottom = 0;
                cellPie.BorderWidth = 0;
                cellPie.BackgroundColor = BaseColor.WHITE;
                cellPie.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPie.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cellPie.AddElement(tblPie);

                //agregamos al Pie la celda que contiene ingresos
                t.AddCell(cellPie);




                pdfDoc.Add(t);

                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();
                System.IO.File.WriteAllBytes($"{path}Uploads/Document/{documentCode}.pdf", bytes);
                pdfWriter.CloseStream = false;

                /*HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={DocumentCode}.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.End();*/
                //*********************************************


                xmlDocumentInsert.Append("<documento>");
                xmlDocumentInsert.Append($"<DocumentGroupID>{documentGroupID}</DocumentGroupID>");
                xmlDocumentInsert.Append($"<DocumentType>{documentGroupType}</DocumentType>");
                xmlDocumentInsert.Append($"<PersonID>{cedula}</PersonID>");
                xmlDocumentInsert.Append($"<DocumentCode>{documentCode}</DocumentCode>");
                xmlDocumentInsert.Append($"<DocumentEmailSend>{employee.PersonEmail}</DocumentEmailSend>");
                xmlDocumentInsert.Append($"<DocumentEmailSendState>{0}</DocumentEmailSendState>");
                xmlDocumentInsert.Append($"<DocumentDateCreation>{documentDateCreation}</DocumentDateCreation>");
                xmlDocumentInsert.Append($"<DocumentActive>{1}</DocumentActive>");
                xmlDocumentInsert.Append("</documento>");
            }
            xmlDocumentInsert.Append("</documentos>");
            //
            await new DocumentModule().InsertXML(xmlDocumentInsert.ToString());
            return new List<string>();
        }


        

        [HttpGet]
        [Route("view-image")]
        [AllowAnonymous]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<DataModel<byte[]>> ReadDocumentPdf(string name)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            DataModel<byte[]> data = new DataModel<byte[]>();
            try
            {
                string pathPhoto = "Uploads/Document";
                string fileNamePhoto = name + ".pdf";//CERT-619.24_440
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathPhoto), fileNamePhoto);
                //2480 x 3508
                //827 x 1170
                // 595 x 842px
                data.Data = new List<byte[]>() { ConvertPdfToImage(new PDFiumSharp.PdfDocument(path, null), 0) };
                data.State = DataState.ok;
                data.Message = string.Empty;
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                data.Data = new List<byte[]>() { };
                data.State = DataState.error;
                data.Message = ex.Message;
            }

            return data;

        }


        [HttpGet]
        [Route("view-pdf-bytes")]
        [AllowAnonymous]
        [EnableCors("http://localhost:4200", "*", "*")]
        public async Task<byte[]> ReadDocumentPdfBytes(string name)
        {
            //new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                string pathPhoto = "Uploads/Document";
                string fileNamePhoto = name + ".pdf";//CERT-619.24_440
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathPhoto), fileNamePhoto);
                //2480 x 3508
                //827 x 1170
                // 595 x 842px
                return await Task.Run(() => File.ReadAllBytes(path));
            }
            catch (Exception ex)
            {
                //new HttpResponseMessage(HttpStatusCode.OK);
                return null;
            }



        }

        [HttpGet]
        [Route("view-pdf")]
        [EnableCors("http://localhost:4200", "*", "*")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> ReadDocumentPdfFile(string name)
        {
            
            try
            {
                //
                string pathPhoto = "Uploads/Document";
                string fileNamePhoto = name + ".pdf";//CERT-619.24_440
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathPhoto), fileNamePhoto);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                response.Content = new StreamContent(fileStream);
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");//angular
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "firmaelectronica.pdf";

                return response;


            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }



        }




        public static byte[] ConvertPdfToImage(PDFiumSharp.PdfDocument pdfDocument, int pageNumber)
        {
            var page = pdfDocument.Pages[pageNumber];

            using (var thumb = new PDFiumBitmap((int)page.Width, (int)page.Height, false))
            {
                page.Render(thumb);

                using (MemoryStream memoryStreamBMP = new MemoryStream())
                {
                    thumb.Save(memoryStreamBMP);

                    using (System.Drawing.Image imageBmp = System.Drawing.Image.FromStream(memoryStreamBMP))
                    {

                        using (MemoryStream memoryStreamJPG = new MemoryStream())
                        {
                            //System.Drawing.Imaging.Encoder myEncoder;
                            //EncoderParameter myEncoderParameter;
                            //EncoderParameters myEncoderParameters;
                            // Create an Encoder object based on the GUID
                            // for the ColorDepth parameter category.
                            //myEncoder = System.Drawing.Imaging.Encoder.ColorDepth;

                            // Create an EncoderParameters object.
                            // An EncoderParameters object has an array of EncoderParameter
                            // objects. In this case, there is only one
                            // EncoderParameter object in the array.
                            //myEncoderParameters = new EncoderParameters(1);

                            // Save the image with a color depth of 24 bits per pixel.
                            //**myEncoderParameter = new EncoderParameter(myEncoder, 24L);
                            //myEncoderParameters.Param[0] = myEncoderParameter;
                            //ImageCodecInfo myImageCodecInfo;
                            // Get an ImageCodecInfo object that represents the TIFF codec.
                            //myImageCodecInfo = GetEncoderInfo("image/jpeg");

                            imageBmp.Save(memoryStreamJPG, ImageFormat.Jpeg);
                            //imageBmp.Save(memoryStreamJPG, myImageCodecInfo, myEncoderParameters);
                            return memoryStreamJPG.ToArray();
                            //return Convert.FromBase64String(memoryStreamJPG.ToArray());
                            //byte[] temp_backToBytes = Convert.FromBase64String(temp_inBase64);
                            // byte[] base64EncodedStringBytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(memoryStreamJPG.ToArray()));
                            //     return base64EncodedStringBytes;
                            //return Image.FromStream(memoryStreamJPG);

                        }
                    }
                }
            }
        }

        /*
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        */




    }
}




/*[HttpGet]
        [Route("pdf-img")]
        [EnableCors("http://localhost:4200", "*", "*")]
        public HttpResponseMessage imagen()
        {
            throw new Exception("");

        }*/


/*public byte[] Pdf2()
{
    string pathPhoto = "Uploads/Document";
    string fileNamePhoto = "CERT-619.24_440.pdf";
    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathPhoto), fileNamePhoto);
    //
    return Render(new PdfDocument(path, null), 0, 300, 700);
    //return Render(new PdfDocument(path, null), 0, 300, 700);

}*/