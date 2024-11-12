using DSCApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DSCApi.Modules
{
    public class UserModule : IApiCrud<UserModel, int?, bool?>
    {
        public string connection => new ConnectionAdo().ConnectionDSC;

        public Task<long> Count()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(UserModel data)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAutomatic(UserAutomaticModel data)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UserAutoGenerate", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //
                        cmd.Parameters.Add(new SqlParameter("@userPassword", EncryptModule.EncryptString(data.UserPassword, EncryptModule.passPhrase)));
                        cmd.Parameters.Add(new SqlParameter("@cedulaRuc", data.CedulaRuc));
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


        public Task<bool> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> Read(int? id, bool? id2, int page, int quantity)
        {
            List<UserModel> data = new List<UserModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UserRead", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 30;
                        cmd.Parameters.Add(new SqlParameter("@UserID", id));
                        cmd.Parameters.Add(new SqlParameter("@UserActive", id2));
                        cmd.Parameters.Add(new SqlParameter("@Page", page));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", (quantity > 1000) ? 1000 : quantity));
                        await cnn.OpenAsync();
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {

                            while (await dr.ReadAsync())
                            {
                                UserModel item = new UserModel();
                                item.UserID = Convert.ToInt32(dr["UserID"]);
                                item.UserName = dr["UserName"].ToString();
                                item.RoleID = dr["RoleID"].ToString();
                                item.UserAlias = dr["UserAlias"].ToString();
                                item.UserPassword = EncryptModule.DecryptString(dr["UserPassword"].ToString(), EncryptModule.passPhrase);
                                item.UserRef = dr["UserRef"].ToString();
                                item.UserActive = Convert.ToBoolean(dr["UserActive"]);
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

        public Task<bool> Update(UserModel data)
        {
            throw new NotImplementedException();
        }


        public async Task<string> Login(LoginModel loginModel) {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UserLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = 300;
                        //para ver la encriptacion de una password
                        //string valor = EncryptModule.EncryptString(loginModel.UserPassword, EncryptModule.passPhrase);
                        cmd.Parameters.Add(new SqlParameter("@UserName", loginModel.UserName));
                        cmd.Parameters.Add(new SqlParameter("@UserPassword", EncryptModule.EncryptString(loginModel.UserPassword, EncryptModule.passPhrase) ));
                        SqlParameter rol = new SqlParameter("@Rol", SqlDbType.VarChar, 10);
                        rol.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(rol);
                        //
                        await cnn.OpenAsync();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        return rol.Value.ToString();
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