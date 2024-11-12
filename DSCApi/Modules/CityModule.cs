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
    public class CityModule : IApiCrud<CityModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(CityModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CityCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CityName", data.CityName));
                        cmd.Parameters.Add(new SqlParameter("@CityActive", data.CityActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_CityDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CityID", id));
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

        public async Task<IEnumerable<CityModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<CityModel> data = new List<CityModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CityRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CityID", id));
                        cmd.Parameters.Add(new SqlParameter("@CityActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                CityModel item = new CityModel();
                                item.CityID = Convert.ToInt32(dr["CityID"]);
                                item.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                                item.CityName = dr["CityName"].ToString();
                                item.CityActive = Convert.ToBoolean(dr["CityActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_CityCountRead", cnn))
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

        public async Task<bool> Update(CityModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CityUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CityID", data.CityID));
                        cmd.Parameters.Add(new SqlParameter("@CityName", data.CityName));
                        cmd.Parameters.Add(new SqlParameter("@CityActive", data.CityActive));
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