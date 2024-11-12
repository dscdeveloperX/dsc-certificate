using DSCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DSCApi.Modules
{
    public class CompanyModule : IApiCrud<CompanyModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(CompanyModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyRuc", data.CompanyRuc));
                        cmd.Parameters.Add(new SqlParameter("@ProvinceID", data.ProvinceID));
                        cmd.Parameters.Add(new SqlParameter("@CityID", data.CityID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyName", data.CompanyName));
                        cmd.Parameters.Add(new SqlParameter("@CompanyAddress", data.CompanyAddress));
                        cmd.Parameters.Add(new SqlParameter("@CompanyPhone", data.CompanyPhone));
                        cmd.Parameters.Add(new SqlParameter("@CompanyActive", data.CompanyActive));
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
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
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

        public async Task<IEnumerable<CompanyModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<CompanyModel> data = new List<CompanyModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@CompanyActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                CompanyModel item = new CompanyModel();
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.CompanyRuc = dr["CompanyRuc"].ToString();
                                item.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                                item.CityID = Convert.ToInt32(dr["CityID"]);
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.CompanyAddress = dr["CompanyAddress"].ToString();
                                item.CompanyPhone = dr["CompanyPhone"].ToString();
                                item.CompanyPhoto = dr["CompanyPhoto"].ToString();
                                item.CompanyUrlVerification = dr["CompanyUrlVerification"].ToString();
                                item.CompanyCodeQrVerification = dr["CompanyCodeQrVerification"].ToString();
                                item.CompanyActive = Convert.ToBoolean(dr["CompanyActive"]);
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

        public async Task<IEnumerable<CompanyModel>> ReadFull(int? id, bool? id2, int page, int quantity)
        {
            List<CompanyModel> data = new List<CompanyModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyReadFull", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@CompanyActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                CompanyModel item = new CompanyModel();
                                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                                item.CompanyRuc = dr["CompanyRuc"].ToString();
                                item.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                                item.ProvinceName = dr["ProvinceName"].ToString();
                                item.CityID = Convert.ToInt32(dr["CityID"]);
                                item.CityName = dr["CityName"].ToString();
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.CompanyAddress = dr["CompanyAddress"].ToString();
                                item.CompanyPhone = dr["CompanyPhone"].ToString();
                                item.CompanyPhoto = dr["CompanyPhoto"].ToString();
                                item.CompanyUrlVerification = dr["CompanyUrlVerification"].ToString();
                                item.CompanyCodeQrVerification = dr["CompanyCodeQrVerification"].ToString();
                                item.CompanyActive = Convert.ToBoolean(dr["CompanyActive"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyCountRead", cnn))
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

        public async Task<bool> Update(CompanyModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", data.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyRuc", data.CompanyRuc));
                        cmd.Parameters.Add(new SqlParameter("@ProvinceID", data.ProvinceID));
                        cmd.Parameters.Add(new SqlParameter("@CityID", data.CityID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyName", data.CompanyName));
                        cmd.Parameters.Add(new SqlParameter("@CompanyAddress", data.CompanyAddress));
                        cmd.Parameters.Add(new SqlParameter("@CompanyPhone", data.CompanyPhone));
                        cmd.Parameters.Add(new SqlParameter("@CompanyActive", data.CompanyActive));

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


/*
        public async Task<IEnumerable<CompanyDepartmentModel>> CompanyDepartmentRead(string id, bool? id2, int page, int quantity)
        {
            List<CompanyDepartmentModel> data = new List<CompanyDepartmentModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CompanyDepartmentRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", id));
                        cmd.Parameters.Add(new SqlParameter("@CompanyActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                CompanyDepartmentModel item = new CompanyDepartmentModel();
                                item.CompanyID = dr["CompanyID"].ToString();
                                item.CompanyName = dr["CompanyName"].ToString();
                                item.CompanyActive = Convert.ToBoolean(dr["CompanyActive"]);
                                item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
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
        */