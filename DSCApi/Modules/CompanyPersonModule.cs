using DSCApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DSCApi.Modules
{
    public class CompanyPersonModule : IApiCrud<CompanyPersonModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public Task<long> Count()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(CompanyPersonModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyPersonCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@PersonActive", data.PersonActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyPersonDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyPersonID", id));
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

        public async Task<IEnumerable<CompanyPersonModel>> Read(int? id, bool? id2)
        {
            List<CompanyPersonModel> data = new List<CompanyPersonModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyPersonRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@PersonActive", id2));
                        //
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                CompanyPersonModel item = new CompanyPersonModel();
                                item.CompanyPersonID = Convert.ToInt32(dr["CompanyPersonID"]);
                                item.CompanyID = dr["CompanyID"].ToString();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonActive = Convert.ToBoolean(dr["PersonActive"]);
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

        public Task<IEnumerable<CompanyPersonModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(CompanyPersonModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyPersonUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyPersonID", data.CompanyPersonID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@PersonActive", data.PersonActive));

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