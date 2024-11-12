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
    public class PersonModule : IApiCrud<PersonModel, string, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public async Task<bool> Create(PersonModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonCreate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@PersonSignatureImage", data.PersonSignatureImage));
                        cmd.Parameters.Add(new SqlParameter("@PersonPhoto", data.PersonPhoto));
                        cmd.Parameters.Add(new SqlParameter("@PersonEmail", data.PersonEmail));
                        cmd.Parameters.Add(new SqlParameter("@ProvinceID", data.ProvinceID));
                        cmd.Parameters.Add(new SqlParameter("@CityID", data.CityID));
                        cmd.Parameters.Add(new SqlParameter("@PersonName", data.PersonName));
                        cmd.Parameters.Add(new SqlParameter("@PersonSurname", data.PersonSurname));
                        cmd.Parameters.Add(new SqlParameter("@PersonDateOfBirth", data.PersonDateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@PersonPhone", data.PersonPhone));
                        cmd.Parameters.Add(new SqlParameter("@GenderID", data.GenderID));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", data.MaritalStatusID));
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

        public async Task<bool> Delete(string id)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonDelete", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@PersonID", id));
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

        public async Task<IEnumerable<PersonModel>> Read(string id, bool? id2, int page, int quantity)
        {
            List<PersonModel> data = new List<PersonModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@PersonID", id));
                        cmd.Parameters.Add(new SqlParameter("@PersonActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                PersonModel item = new PersonModel();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonSignatureImage = dr["PersonSignatureImage"].ToString();
                                item.PersonPhoto = dr["PersonPhoto"].ToString();
                                item.CityID = Convert.ToInt32(dr["CityID"]);
                                item.CityName = dr["CityName"].ToString();
                                item.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                                item.ProvinceName = dr["ProvinceName"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.PersonEmail = dr["PersonEmail"].ToString();
                                item.PersonPhone = dr["PersonPhone"].ToString();
                                item.PersonDateOfBirth = Convert.ToDateTime(dr["PersonDateOfBirth"]);
                                item.GenderID = dr["GenderID"].ToString();
                                item.GenderDescription = dr["GenderDescription"].ToString();
                                item.MaritalStatusID = dr["MaritalStatusID"].ToString();
                                item.MaritalStatusDescription = dr["MaritalStatusDescription"].ToString();
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

        public async Task<IEnumerable<PersonModel>> ReadSearch(string personName, int page, int quantity)
        {
            List<PersonModel> data = new List<PersonModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonReadSearch", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 500) ? 500 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                PersonModel item = new PersonModel();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonSignatureImage = dr["PersonSignatureImage"].ToString();
                                item.PersonPhoto = dr["PersonPhoto"].ToString();
                                item.CityID = Convert.ToInt32(dr["CityID"]);
                                item.CityName = dr["CityName"].ToString();
                                item.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                                item.ProvinceName = dr["ProvinceName"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
                                item.PersonEmail = dr["PersonEmail"].ToString();
                                item.PersonPhone = dr["PersonPhone"].ToString();
                                item.PersonDateOfBirth = Convert.ToDateTime(dr["PersonDateOfBirth"]);
                                item.GenderID = dr["GenderID"].ToString();
                                item.GenderDescription = dr["GenderDescription"].ToString();
                                item.MaritalStatusID = dr["MaritalStatusID"].ToString();
                                item.MaritalStatusDescription = dr["MaritalStatusDescription"].ToString();
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

        public async Task<IEnumerable<PersonEmployeeModel>> PersonEmployeeRead(int companyID)
        {
            List<PersonEmployeeModel> data = new List<PersonEmployeeModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonEmployeeRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", companyID));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {


                            while (await dr.ReadAsync())
                            {
                                PersonEmployeeModel item = new PersonEmployeeModel();
                                item.PersonID = dr["PersonID"].ToString();
                                item.PersonPhoto = dr["PersonPhoto"].ToString();
                                item.PersonName = dr["PersonName"].ToString();
                                item.PersonSurname = dr["PersonSurname"].ToString();
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
                    using (SqlCommand cmd = new SqlCommand("sp_PersonCountRead", cnn))
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

        public async Task<long> Count(string personName)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonCountRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
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

        public async Task<bool> Update(PersonModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_PersonUpdate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@PersonID", data.PersonID));
                        cmd.Parameters.Add(new SqlParameter("@ProvinceID", data.ProvinceID));
                        cmd.Parameters.Add(new SqlParameter("@CityID", data.CityID));
                        cmd.Parameters.Add(new SqlParameter("@PersonName", data.PersonName));
                        cmd.Parameters.Add(new SqlParameter("@PersonSurname", data.PersonSurname));
                        cmd.Parameters.Add(new SqlParameter("@PersonEmail", data.PersonEmail));
                        cmd.Parameters.Add(new SqlParameter("@PersonDateOfBirth", data.PersonDateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@PersonPhone", data.PersonPhone));
                        cmd.Parameters.Add(new SqlParameter("@GenderID", data.GenderID));
                        cmd.Parameters.Add(new SqlParameter("@MaritalStatusID", data.MaritalStatusID));
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