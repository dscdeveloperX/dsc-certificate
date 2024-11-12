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
    public class ParameterModule : IApiCrud<ParameterModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(ParameterModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@ParameterName", data.ParameterName));
                        cmd.Parameters.Add(new SqlParameter("@ParameterValue", data.ParameterValue));
                        cmd.Parameters.Add(new SqlParameter("@ParameterType", data.ParameterType));
                        cmd.Parameters.Add(new SqlParameter("@ParameterActive", data.ParameterActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@ParameterID", id));
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

        public async Task<IEnumerable<ParameterModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<ParameterModel> data = new List<ParameterModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@ParameterID", id));
                        cmd.Parameters.Add(new SqlParameter("@ParameterActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                ParameterModel item = new ParameterModel();
                                item.ParameterID = Convert.ToInt32(dr["ParameterID"]);
                                item.CompanyID = dr["CompanyID"].ToString();
                                item.ParameterName = dr["ParameterName"].ToString();
                                item.ParameterValue = dr["ParameterValue"].ToString();
                                item.ParameterType = dr["ParameterType"].ToString();
                                item.ParameterActive = Convert.ToBoolean(dr["ParameterActive"]);
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

        public async Task<long> Count() {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterCountRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
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
        
        public async Task<IEnumerable<ParameterModel>> ParameterCompanyRead(string id, bool? id2, int page, int quantity)
        {
            List<ParameterModel> data = new List<ParameterModel>();
            try
            {
                
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterCompanyRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@ParameterActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity>500)?500:quantity));
                        //
                        await cnn.OpenAsync();
                        
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            

                            while (await dr.ReadAsync())
                            {
                                ParameterModel item = new ParameterModel();
                                item.ParameterID = Convert.ToInt32(dr["ParameterID"]);
                                item.CompanyID = dr["CompanyID"].ToString();
                                item.ParameterName = dr["ParameterName"].ToString();
                                item.ParameterValue = dr["ParameterValue"].ToString();
                                item.ParameterType = dr["ParameterType"].ToString();
                                item.ParameterActive = Convert.ToBoolean(dr["ParameterActive"]);
                       
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

        public async Task<bool> Update(ParameterModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ParameterUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@ParameterID", data.ParameterID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@ParameterName", data.ParameterName));
                        cmd.Parameters.Add(new SqlParameter("@ParameterValue", data.ParameterValue));
                        cmd.Parameters.Add(new SqlParameter("@ParameterType", data.ParameterType));
                        cmd.Parameters.Add(new SqlParameter("@ParameterActive", data.ParameterActive));


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