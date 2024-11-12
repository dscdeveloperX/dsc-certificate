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
    public class OccupationModule : IApiCrud<OccupationModel, string, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(OccupationModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_OccupationCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@OccupationID", data.OccupationID));
                        cmd.Parameters.Add(new SqlParameter("@OccupationDescription", data.OccupationDescription));
                        cmd.Parameters.Add(new SqlParameter("@OccupationActive", data.OccupationActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_OccupationDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@OccupationID", id));
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

        public async Task<IEnumerable<OccupationModel>> Read(string id, bool? id2, int page, int quantity)
        {
            List<OccupationModel> data = new List<OccupationModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_OccupationRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@OccupationID", id));
                        cmd.Parameters.Add(new SqlParameter("@OccupationActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                OccupationModel item = new OccupationModel();
                                item.OccupationID = dr["OccupationID"].ToString();
                                item.OccupationDescription = dr["OccupationDescription"].ToString();
                                item.OccupationActive = Convert.ToBoolean(dr["OccupationActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_OccupationCountRead", cnn))
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

        public async Task<bool> Update(OccupationModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_OccupationUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@OccupationID", data.OccupationID));
                        cmd.Parameters.Add(new SqlParameter("@OccupationDescription", data.OccupationDescription));
                        cmd.Parameters.Add(new SqlParameter("@OccupationActive", data.OccupationActive));
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