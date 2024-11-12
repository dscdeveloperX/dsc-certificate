using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DSCApi.Models;
using DSCApi.Modules;

namespace DSCApi.Modules
{
    public class MaritalStatusModule : IApiCrud<MaritalStatusModel, string, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(MaritalStatusModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MaritalStatusCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", data.MaritalStatusID));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusDescription", data.MaritalStatusDescription));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusActive", data.MaritalStatusActive));
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

        public async Task<bool> Delete(string id)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MaritalStatusDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", id));
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

        public async Task<IEnumerable<MaritalStatusModel>> Read(string id, bool? id2, int page, int quantity)
        {
            List<MaritalStatusModel> data = new List<MaritalStatusModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MaritalStatusRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", id));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                MaritalStatusModel item = new MaritalStatusModel();
                                item.MaritalStatusID = (dr["MaritalStatusID"]).ToString();
                                item.MaritalStatusDescription = dr["MaritalStatusDescription"].ToString();
                                item.MaritalStatusActive = Convert.ToBoolean(dr["MaritalStatusActive"]);
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


        public async Task<long> Count()
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MaritalStatusCountRead", cnn))
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

        public async Task<bool> Update(MaritalStatusModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MaritalStatusUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", data.MaritalStatusID));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusDescription", data.MaritalStatusDescription));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusActive", data.MaritalStatusActive));
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