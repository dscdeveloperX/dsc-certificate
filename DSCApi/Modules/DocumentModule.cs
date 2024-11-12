using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DSCApi.Models;
using DSCApi.Modules;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;
using PDFiumSharp;
using System.Drawing.Imaging;
using System.Drawing;

namespace DSCApi.Modules
{
    public class DocumentModule : IApiCrud<DocumentModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(DocumentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentID", data.GroupDocumentID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentType", data.DocumentType));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentDateCreation", data.DocumentDateCreation));
                        cmd.Parameters.Add(new SqlParameter("@DocumentActive", data.DocumentActive));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> Delete(int? id)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DocumentID", id));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DocumentModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<DocumentModel> data = new List<DocumentModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@DocumentID", id));
                        cmd.Parameters.Add(new SqlParameter("@DocumentActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DocumentModel item = new DocumentModel();
                                item.DocumentID = Convert.ToInt32(dr["DocumentID"]);
                                item.GroupDocumentID = Convert.ToInt32(dr["GroupDocumentID"]);
                                item.DocumentType = dr["DocumentType"].ToString();
                                item.PersonID = dr["PersonID"].ToString();
                                item.DocumentCode = dr["DocumentCode"].ToString();
                                item.DocumentDateCreation = Convert.ToDateTime(dr["DocumentDateCreation"]);
                                item.DocumentActive = Convert.ToBoolean(dr["DocumentActive"]);
                                //
                                data.Add(item);
                            }
                            dr.Close();
                            cnn.Close();
                            cnn.Dispose();

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<DocumentAdminModel>> DocumentAdminRead(int documentGroupID, int page, int quantity)
        {
            List<DocumentAdminModel> data = new List<DocumentAdminModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentAdminRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", documentGroupID));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DocumentAdminModel item = new DocumentAdminModel();
                                
                                item.DocumentID = Convert.ToInt32(dr["DocumentID"]);
                                item.DocumentGroupID = Convert.ToInt32(dr["DocumentGroupID"]);
                                item.DocumentType = dr["DocumentType"].ToString();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.DocumentCode = dr["DocumentCode"].ToString();
                                item.DocumentEmailSend = dr["DocumentEmailSend"].ToString();
                                item.DocumentEmailSendState = Convert.ToBoolean(dr["DocumentEmailSendState"]);
                                item.DocumentDateEmailSend = dr["DocumentDateEmailSend"] != DBNull.Value ? Convert.ToDateTime(dr["DocumentDateEmailSend"]) : (DateTime?)null;
                                item.DocumentDateCreation = Convert.ToDateTime(dr["DocumentDateCreation"]);
                                //
                                data.Add(item);
                            }
                            dr.Close();
                            cnn.Close();
                            cnn.Dispose();

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<IEnumerable<DocumentGuestModel>> DocumentGuestRead(string documentCode)
        {
            List<DocumentGuestModel> data = new List<DocumentGuestModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGuestRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@DocumentCode", documentCode));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                DocumentGuestModel item = new DocumentGuestModel();

                                item.DocumentID = Convert.ToInt32(dr["DocumentID"]);
                                item.DocumentType = dr["DocumentType"].ToString();
                                item.DocumentTypeDescription = dr["DocumentTypeDescription"].ToString();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.DocumentCode = dr["DocumentCode"].ToString();
                                item.EmployeeActive = Convert.ToBoolean(dr["EmployeeActive"]);
                                item.DocumentDateCreation = Convert.ToDateTime(dr["DocumentDateCreation"]);
                                //
                                data.Add(item);
                            }
                            dr.Close();
                            cnn.Close();
                            cnn.Dispose();

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public async Task<IEnumerable<DocumentUserModel>> DocumentUserRead(int companyID, string documentType, int documentGroupDateYear)
        {
            List<DocumentUserModel> data = new List<DocumentUserModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentUserRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentType", documentType));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDateYear", documentGroupDateYear));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DocumentUserModel item = new DocumentUserModel();
                                item.DocumentID = Convert.ToInt32(dr["DocumentID"]);
                                item.DocumentGroupID = Convert.ToInt32(dr["DocumentGroupID"]);
                                item.DocumentType = dr["DocumentType"].ToString();
                                item.DocumentGroupDescription = dr["DocumentGroupDescription"].ToString();
                                item.DocumentGroupDate = Convert.ToDateTime(dr["DocumentGroupDate"]);
                                item.DocumentCode = dr["DocumentCode"].ToString();
                                //
                                data.Add(item);
                            }
                            dr.Close();
                            cnn.Close();
                            cnn.Dispose();

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<long> Count() { throw new Exception(); }

        public async Task<long> Count(int documentGroupID)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentCountRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        SqlParameter RowsCount = new SqlParameter("@RowsCount", SqlDbType.BigInt);
                        RowsCount.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(RowsCount);
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", documentGroupID));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return (long)RowsCount.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(DocumentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DocumentID", data.DocumentID));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentID", data.GroupDocumentID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentType", data.DocumentType));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentDateCreation", data.DocumentDateCreation));
                        cmd.Parameters.Add(new SqlParameter("@DocumentActive", data.DocumentActive));


                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string CellValue(SpreadsheetDocument spreadsheetDocument, string sheetName, string column, int row)
        {

            //using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(document, false))
            //{
                //Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                Sheet theSheet = spreadsheetDocument.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }
                WorksheetPart wsPart = (WorksheetPart)(spreadsheetDocument.WorkbookPart.GetPartById(theSheet.Id));
                
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == $"{column}{row}").FirstOrDefault();

                string value = "";
                if (theCell != null)
                {
                    
                    if (theCell.InnerText.Length > 0)//celda vacia
                    {
                        value = theCell.InnerText;

                        if (theCell.DataType != null)
                        {
                            // Code removed here…
                            switch (theCell.DataType.Value)//celda string
                            {
                                case CellValues.SharedString:

                                    // For shared strings, look up the value in the
                                    // shared strings table.
                                    var stringTable = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>()
                                        .FirstOrDefault();

                                    // If the shared string table is missing, something 
                                    // is wrong. Return the index that is in
                                    // the cell. Otherwise, look up the correct text in 
                                    // the table.
                                    if (stringTable != null)
                                    {
                                        value =
                                            stringTable.SharedStringTable
                                            .ElementAt(int.Parse(value)).InnerText;
                                    }
                                    break;

                                case CellValues.Boolean:
                                    switch (value)
                                    {
                                        case "0":
                                            value = "FALSE";
                                            break;
                                        default:
                                            value = "TRUE";
                                            break;
                                    }
                                    break;
                            }
                        }
                        else if(theCell.CellFormula!=null){
                            value = theCell.CellValue.InnerText;
                        }
                    }
                
                   
                    //value = theCell.InnerText;
                  
                }

                /*var stringTable = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                if (stringTable != null)
                {
                    value =
                        stringTable.SharedStringTable
                        .ElementAt(int.Parse(value)).InnerText;
                }*/
                return value;
            //}
        }


        public async Task<List<string>> PdfValidarRolPago(XmlDocument xmlDoc, int companyId)
        {
            //obtener lista de empleados
            List<string> empleados = await new EmployeeModule().EmployeePersonIDRead(companyId);
            //
            List<String> logXml = new List<string>();
            //leer archivo xml
            /*XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);*/
            //xmlDoc.GetElementsByTagName("documento");
            //raiz document
            XmlNode root = xmlDoc.DocumentElement;

            if (root.Name == "documentos")
            {
                //lista de documentos.FirstChild
                XmlNodeList documentos = root.ChildNodes;
                if (documentos.Count > 0)
                {
                    string cedula = "";
                    //recorrer documentos
                    for (int i = 0; i < documentos.Count; i++)
                    {
                        //si informacion tiene elementos
                        if (documentos[i].HasChildNodes)
                        {
                            /******************************************************************
                             * INFORMACION
                            ******************************************************************/
                            if (documentos[i].ChildNodes[0].Name == "informacion")
                            {
                                //CEDULA--------------------------------------------------------------
                                cedula = documentos[i].ChildNodes[0].ChildNodes[0].InnerText;
                                //formato correcto de cedula
                                if (cedula.Length > 0 && cedula.Length <= 10)
                                {
                                    //buscar cedula en lista de empleados
                                    bool cedulaExists = false;
                                    for (int a = 0; a < empleados.Count; a++)
                                    {
                                        if (cedula == empleados[a])
                                        {
                                            cedulaExists = true;
                                            break;
                                        }
                                    }
                                    //si no existe cedula agregar error
                                    if (!cedulaExists)
                                    {
                                        logXml.Add(cedula + ": No existe cédula");
                                    }
                                }
                                else
                                {
                                    logXml.Add(cedula + ": cédula con formato incorrecto");
                                }
                                //SUELDO BASE--------------------------------------------------------------
                                string sueldo_base = documentos[i].ChildNodes[0].ChildNodes[1].InnerText;
                                Decimal r;
                                if (!Decimal.TryParse(sueldo_base,out r)) {
                                    logXml.Add(cedula + ": sueldo base con formato incorrecto");
                                }
                                //DIAS BASE--------------------------------------------------------------
                                string dias_base = documentos[i].ChildNodes[0].ChildNodes[2].InnerText;
                                int r2;
                                if (!int.TryParse(dias_base, out r2))
                                {
                                    logXml.Add(cedula + ": días base con formato incorrecto");
                                }
                                //CARGO--------------------------------------------------------------
                                if (documentos[i].ChildNodes[0].ChildNodes[3].InnerText == string.Empty)
                                {
                                    logXml.Add(cedula + ": cargo está vacío");
                                }
                                //DEPARTAMENTO--------------------------------------------------------------
                                if (documentos[i].ChildNodes[0].ChildNodes[4].InnerText == string.Empty)
                                {
                                    logXml.Add(cedula + ": departamento está vacío");
                                }
                                //TOTAL INGRESOS--------------------------------------------------------------
                                if (documentos[i].ChildNodes[0].ChildNodes[5].InnerText == string.Empty)
                                {
                                    logXml.Add(cedula + ": total ingresos está vacío");
                                }
                                //TOTAL DESCUENTOS--------------------------------------------------------------
                                if (documentos[i].ChildNodes[0].ChildNodes[6].InnerText == string.Empty)
                                {
                                    logXml.Add(cedula + ": total descuentos está vacío");
                                }
                                //TOTAL A RECIBIR-------------------------------------------------------------
                                if (documentos[i].ChildNodes[0].ChildNodes[7].InnerText == string.Empty)
                                {
                                    logXml.Add(cedula + ": total a recibir está vacío");
                                }
                            }
                            else {
                                logXml.Add("No existe elemento \"informacion\"");
                            }

                            /******************************************************************
                            INGRESOS    
                            ******************************************************************/
                            if (documentos[i].ChildNodes[1].Name == "ingresos")
                            {
                                //si ingresos tiene elementos
                                if (documentos[i].ChildNodes[1].HasChildNodes)
                                {
                                    //recorremos los rubros
                                    for (int b=0; b< documentos[i].ChildNodes[1].ChildNodes.Count; b++) {
                                        //NOMBRE--------------------------------------------------------------
                                        if (documentos[i].ChildNodes[1].ChildNodes[b].ChildNodes[0].InnerText == string.Empty)
                                        {
                                            logXml.Add(cedula + ": nombre de ingreso está vacío");
                                        }
                                        //VALOR--------------------------------------------------------------
                                        string valor = documentos[i].ChildNodes[1].ChildNodes[b].ChildNodes[1].InnerText;
                                        Decimal r;
                                        if (!Decimal.TryParse(valor, out r))
                                        {
                                            logXml.Add(cedula + ": valor de ingreso con formato incorrecto");
                                        }
                                    }
                                }
                                else {
                                    logXml.Add("\"ingresos\" no tiene rubros");
                                }
                            }
                            else {
                                logXml.Add("No existe elemento \"ingresos\"");
                            }

                            /******************************************************************
                            DESCUENTOS    
                            ******************************************************************/
                            if (documentos[i].ChildNodes[2].Name == "descuentos")
                            {
                                //si descuentos tiene elementos
                                if (documentos[i].ChildNodes[2].HasChildNodes)
                                {
                                    //recorremos los rubros
                                    for (int b = 0; b < documentos[i].ChildNodes[2].ChildNodes.Count; b++)
                                    {
                                        //NOMBRE--------------------------------------------------------------
                                        if (documentos[i].ChildNodes[2].ChildNodes[b].ChildNodes[0].InnerText == string.Empty)
                                        {
                                            logXml.Add(cedula + ": nombre de descuento está vacío");
                                        }
                                        //VALOR--------------------------------------------------------------
                                        string valor = documentos[i].ChildNodes[2].ChildNodes[b].ChildNodes[1].InnerText;
                                        Decimal r;
                                        if (!Decimal.TryParse(valor, out r))
                                        {
                                            logXml.Add(cedula + ": valor de descuento con formato incorrecto");
                                        }
                                    }
                                }
                                else
                                {
                                    logXml.Add("\"descuentos\" no tiene rubros");
                                }
                            }
                            else
                            {
                                logXml.Add("No existe elemento \"descuentos\"");
                            }

                        }
                        else
                        {
                            logXml.Add("\"información\" no tiene elementos");
                        }
                    }
                }
                else
                {
                    logXml.Add("No existen documentos");
                }
            }
            else
            {
                logXml.Add("No existe raíz \"documento\"");
            }

            return logXml;
        }



        public async Task InsertXML(string dataXML)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentInsertXml", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.Add(new SqlParameter("@xmlData", dataXML));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task UpdateXML(string dataXML)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentUpdateXml", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.Add(new SqlParameter("@xmlData", dataXML));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task DeleteXML(string dataXML, int documentGroupID)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentDeleteXml", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.Add(new SqlParameter("@xmlData", dataXML));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", documentGroupID));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DocumentXmlModel>> SelectXML(string dataXML)
        {

            List<DocumentXmlModel> data = new List<DocumentXmlModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentSelectXml", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.Add(new SqlParameter("@xmlData", dataXML));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                DocumentXmlModel item = new DocumentXmlModel();
                                item.PersonID = Convert.ToInt32(dr["PersonID"]);
                                item.PersonName = dr["PersonName"].ToString();
                                item.DocumentEmailSend = dr["DocumentEmailSend"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.DocumentGroupDescription = dr["DocumentGroupDescription"].ToString();
                                item.DocumentCode = dr["DocumentCode"].ToString();
                                //
                                data.Add(item);
                            }
                            dr.Close();
                            cnn.Close();
                            cnn.Dispose();

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }







            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentSelectXml", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.Add(new SqlParameter("@xmlData", dataXML));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteDocument(string name) {
            string path = System.Web.HttpContext.Current.Server.MapPath(Path.Combine("~/Uploads/Document",name + ".pdf"));
            if (System.IO.File.Exists(path))
            {
               await Task.Run(()=> { System.IO.File.Delete(path);});
            }
            
        }

    }
}
