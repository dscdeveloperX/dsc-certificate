using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DSCApi.Models;
using DSCApi.Modules;
using System.Data.SqlClient;
using System.Data;


namespace DSCApi.Modules
{
    public class EmployeeModule : IApiCrud<EmployeeModel, int?, bool?>
    {
        public  string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(EmployeeModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeDateEntry", data.EmployeeDateEntry));
                        SqlParameter prmEmployeeDateExit = new SqlParameter();
                        prmEmployeeDateExit.SqlDbType = SqlDbType.DateTime;
                        prmEmployeeDateExit.ParameterName = "@EmployeeDateExit";
                        if (data.EmployeeDateExit == null) { prmEmployeeDateExit.Value = DBNull.Value; } else { prmEmployeeDateExit.Value = data.EmployeeDateExit; }
                        cmd.Parameters.Add(prmEmployeeDateExit);
                        SqlParameter prmEmployeeReason = new SqlParameter();
                        prmEmployeeReason.SqlDbType = SqlDbType.VarChar;
                        prmEmployeeReason.ParameterName = "@EmployeeReason";
                        if (data.EmployeeDateExit == null) { prmEmployeeReason.Value = DBNull.Value; } else { prmEmployeeReason.Value = data.EmployeeReason; }
                        cmd.Parameters.Add(prmEmployeeReason);
                        cmd.Parameters.Add(new SqlParameter("@EmployeeActive", data.EmployeeActive));
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

        public Task<bool> Delete(int? id)
        {
            throw new Exception();
        }

        public async Task<bool> Delete(int companyID, int employeeID)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
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
        
        public async Task<IEnumerable<EmployeeModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<EmployeeModel> data = new List<EmployeeModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", id));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            
                            while (await dr.ReadAsync())
                            {
                                EmployeeModel item = new EmployeeModel();
                                item.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.PersonID = dr["PersonID"].ToString();
                                item.EmployeeDateEntry = Convert.ToDateTime(dr["EmployeeDateEntry"]);
                                item.EmployeeDateExit = Convert.ToDateTime(dr["EmployeeDateExit"]);
                                item.EmployeeReason = dr["EmployeeReason"].ToString();
                                item.EmployeeActive = Convert.ToBoolean(dr["EmployeeActive"]);
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

        public async Task<List<string>> EmployeePersonIDRead(int companyID)
        {
            List<string> data = new List<string>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeePersonIDRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {

                            while (await dr.ReadAsync())
                            {
                                data.Add(dr["PersonID"].ToString());
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

        public async Task<IEnumerable<EmployeeRolPagoModel>> EmployeeRolPagoRead(int companyID)
        {
            List<EmployeeRolPagoModel> data = new List<EmployeeRolPagoModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeRolPagoRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {

                            while (await dr.ReadAsync())
                            {

                                EmployeeRolPagoModel item = new EmployeeRolPagoModel();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.PersonSignatureImage = dr["PersonSignatureImage"].ToString();
                                item.PersonPhoto = dr["PersonPhoto"].ToString();
                                item.PersonEmail = dr["PersonEmail"].ToString();
                                item.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                                item.EmployeeDateEntry = Convert.ToDateTime(dr["EmployeeDateEntry"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.CompanyRuc = dr["CompanyRuc"].ToString();
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.CompanyAddress = dr["CompanyAddress"].ToString();
                                item.CompanyPhone = dr["CompanyPhone"].ToString();
                                item.CompanyPhoto = dr["CompanyPhoto"].ToString();
                                item.CompanyUrlVerification = dr["CompanyUrlVerification"].ToString();
                                item.CompanyCodeQrVerification = dr["CompanyCodeQrVerification"].ToString();
                                
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

        public async Task<IEnumerable<EmployeeModel>> ReadSearch(string personName, int companyID, int page, int quantity)
        {
            List<EmployeeModel> data = new List<EmployeeModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeReadSearch", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {

                            while (await dr.ReadAsync())
                            {
                                EmployeeModel item = new EmployeeModel();
                                item.PersonPhoto = dr["PersonPhoto"].ToString();
                                item.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.EmployeeDateEntry = Convert.ToDateTime(dr["EmployeeDateEntry"]);
                                item.EmployeeDateExit = dr["EmployeeDateExit"] == DBNull.Value ? (DateTime?)null :Convert.ToDateTime(dr["EmployeeDateExit"]);
                                item.EmployeeReason = dr["EmployeeReason"] == DBNull.Value ? string.Empty : dr["EmployeeReason"].ToString();
                                item.EmployeeActive = Convert.ToBoolean(dr["EmployeeActive"]);
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

        public Task<long> Count()
        {
            throw new Exception();
        }

        public async Task<long> Count(string personName, int companyID)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeCountRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        SqlParameter RowsCount = new SqlParameter("@RowsCount", SqlDbType.BigInt);
                        RowsCount.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(RowsCount);
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

        public async Task<bool> Update(EmployeeModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", data.EmployeeID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        SqlParameter prmEmployeeDateExit = new SqlParameter();
                        prmEmployeeDateExit.SqlDbType = SqlDbType.DateTime;
                        prmEmployeeDateExit.ParameterName = "@EmployeeDateExit";
                        if (data.EmployeeDateExit == null) { prmEmployeeDateExit.Value = DBNull.Value; } else { prmEmployeeDateExit.Value = data.EmployeeDateExit; }
                        cmd.Parameters.Add(prmEmployeeDateExit);
                        SqlParameter prmEmployeeReason = new SqlParameter();
                        prmEmployeeReason.SqlDbType = SqlDbType.VarChar;
                        prmEmployeeReason.ParameterName = "@EmployeeReason";
                        if (data.EmployeeDateExit == null) { prmEmployeeReason.Value = DBNull.Value; } else { prmEmployeeReason.Value = data.EmployeeReason; }
                        cmd.Parameters.Add(prmEmployeeReason);
                        cmd.Parameters.Add(new SqlParameter("@EmployeeActive", data.EmployeeActive));


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

    }
}