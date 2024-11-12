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
    public class GroupDocumentModule : IApiCrud<GroupDocumentModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(GroupDocumentModel data) { throw new Exception(); }

        public async Task<int> CreateID(GroupDocumentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //DocumentGroupID
                        SqlParameter documentGroupID = new SqlParameter();
                        documentGroupID.ParameterName = "@DocumentGroupID";
                        documentGroupID.Direction = ParameterDirection.Output;
                        documentGroupID.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(documentGroupID);
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentType", data.GroupDocumentType));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentDate", data.GroupDocumentDate));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentDescription", data.GroupDocumentDescription));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentActive", data.GroupDocumentActive));
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return Convert.ToInt32(documentGroupID.Value);
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
                    using (SqlCommand cmd = new SqlCommand("sp_GroupDocumentDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentID", id));
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

        public async Task<IEnumerable<GroupDocumentModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<GroupDocumentModel> data = new List<GroupDocumentModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GroupDocumentRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentID", id));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                GroupDocumentModel item = new GroupDocumentModel();
                                item.GroupDocumentID = Convert.ToInt32(dr["GroupDocumentID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.GroupDocumentType = dr["GroupDocumentType"].ToString();
                                item.GroupDocumentDate = Convert.ToDateTime(dr["GroupDocumentDate"]);
                                item.GroupDocumentDescription = dr["GroupDocumentDescription"].ToString();
                                item.GroupDocumentActive = Convert.ToBoolean(dr["GroupDocumentActive"]);
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

        public async Task<IEnumerable<GroupDocumentModel>> GroupDocumentCompanyRead(string id, bool? id2, int page, int quantity)
        {
            List<GroupDocumentModel> data = new List<GroupDocumentModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GroupDocumentCompanyRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                GroupDocumentModel item = new GroupDocumentModel();
                                item.GroupDocumentID = Convert.ToInt32(dr["GroupDocumentID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.GroupDocumentType = dr["GroupDocumentType"].ToString();
                                item.GroupDocumentDate = Convert.ToDateTime(dr["GroupDocumentDate"]);
                                item.GroupDocumentDescription = dr["GroupDocumentDescription"].ToString();
                                item.GroupDocumentActive = Convert.ToBoolean(dr["GroupDocumentActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_GroupDocumentCountRead", cnn))
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

        public async Task<bool> Update(GroupDocumentModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GroupDocumentUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentID", data.GroupDocumentID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentType", data.GroupDocumentType));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentDate", data.GroupDocumentDate));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentDescription", data.GroupDocumentDescription));
                        cmd.Parameters.Add(new SqlParameter("@GroupDocumentActive", data.GroupDocumentActive));


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