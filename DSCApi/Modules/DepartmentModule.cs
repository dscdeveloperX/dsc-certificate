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
    public class DepartmentModule : IApiCrud<DepartmentModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(DepartmentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DepartmentCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentName", data.DepartmentName));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentActive", data.DepartmentActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_DepartmentDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", id));
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

        public async Task<IEnumerable<DepartmentModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<DepartmentModel> data = new List<DepartmentModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DepartmentRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", id));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DepartmentModel item = new DepartmentModel();
                                item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                                item.CompanyID = dr["CompanyID"].ToString();
                                item.DepartmentName = dr["DepartmentName"].ToString();
                                item.DepartmentActive = Convert.ToBoolean(dr["DepartmentActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_DepartmentCountRead", cnn))
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

        public async Task<bool> Update(DepartmentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DepartmentUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", data.DepartmentID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentName", data.DepartmentName));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentActive", data.DepartmentActive));

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