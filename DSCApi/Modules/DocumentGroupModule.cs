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
    public class DocumentGroupModule : IApiCrud<DocumentGroupModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(DocumentGroupModel data) { throw new Exception(); }

        public async Task<int> CreateID(DocumentGroupModel data)
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
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupType", data.DocumentGroupType));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDate", data.DocumentGroupDate));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDescription", data.DocumentGroupDescription));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupActive", data.DocumentGroupActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", id));
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

        public async Task<IEnumerable<DocumentGroupModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<DocumentGroupModel> data = new List<DocumentGroupModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", id));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DocumentGroupModel item = new DocumentGroupModel();
                                item.DocumentGroupID = Convert.ToInt32(dr["DocumentGroupID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.DocumentGroupType = dr["DocumentGroupType"].ToString();
                                item.DocumentGroupDate = Convert.ToDateTime(dr["DocumentGroupDate"]);
                                item.DocumentGroupDescription = dr["DocumentGroupDescription"].ToString();
                                item.DocumentGroupActive = Convert.ToBoolean(dr["DocumentGroupActive"]);
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

        public async Task<IEnumerable<DocumentGroupModel>> DocumentGroupCompanyRead(int? id, string documentGroupType, int? documentGroupDateYear, bool? id2, int page, int quantity)
        {
            List<DocumentGroupModel> data = new List<DocumentGroupModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupCompanyRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupType", documentGroupType));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDateYear", documentGroupDateYear));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                DocumentGroupModel item = new DocumentGroupModel();
                                item.DocumentGroupID = Convert.ToInt32(dr["DocumentGroupID"]);
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.DocumentGroupType = dr["DocumentGroupType"].ToString();
                                item.DocumentGroupDate = Convert.ToDateTime(dr["DocumentGroupDate"]);
                                item.DocumentGroupDescription = dr["DocumentGroupDescription"].ToString();
                                item.DocumentGroupActive = Convert.ToBoolean(dr["DocumentGroupActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupCountRead", cnn))
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

        public async Task<bool> Update(DocumentGroupModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DocumentGroupUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupID", data.DocumentGroupID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupType", data.DocumentGroupType));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDate", data.DocumentGroupDate));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupDescription", data.DocumentGroupDescription));
                        cmd.Parameters.Add(new SqlParameter("@DocumentGroupActive", data.DocumentGroupActive));


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