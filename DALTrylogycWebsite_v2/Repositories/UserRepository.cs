using System;
using System.Data.SqlClient;
using System.Data;
using DALTrylogycWebsite.DALResponses;
using DALTrylogycWebsite.DALResponses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using log4net;

namespace DataAccess.Repositories
{

    /// <summary>
    /// The User Repository
    /// </summary>
    /// <seealso cref="DataAccess.Repositories.BaseRepository" />
    public class UserRepository : BaseRepository, IUserRepository
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public UserRepository(ILog log, string connString) : base(log, connString)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IBaseDALResponse GetUserByEmail(string userEmail)
        {
            _log.Info("GetUserByEmail() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter("", Connection);

            SqlParameter Prmparametro = new SqlParameter();

            Prmparametro.ParameterName = "USERNAME";
            Prmparametro.SqlDbType = System.Data.SqlDbType.VarChar;
            Prmparametro.Value = userEmail;

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SEL_USUARIO_CGP";
            Command.Parameters.Add(Prmparametro);


            try
            {
                _log.Info("Opening SQL Connection.");
                Connection.Open();
                _log.Info($"Setting SQL Command to {Command?.CommandText}");
                dA.SelectCommand = Command;
                _log.Info($"Filling dataset.");
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }

            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"GetUserByEmail() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Gets the connections by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IBaseDALResponse GetConnectionsByUserId(int userId)
        {
            _log.Info("GetConnectionsByUserId() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();
            SqlParameter Prmparametro1 = new SqlParameter();

            try
            {

                Prmparametro1.ParameterName = "idUsuario";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro1.Value = userId;


                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "SEL_SOCIOCONEXION";

                Command.Parameters.Add(Prmparametro1);
                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }


            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"GetConnectionsByUserId() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Registers the user with the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="emailConfirm">The email confirm.</param>
        /// <param name="Code">The code.</param>
        /// <param name="CGP">The CGP.</param>
        /// <returns></returns>
        public IBaseDALResponse Register(string email, int associateId, int conectionId, bool emailInvoices)
        {
            _log.Info("Register() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();
            SqlParameter Prmparametro4 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "email";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro1.Value = email;

                Prmparametro2.ParameterName = "XmlSocio";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro2.Value = associateId;

                Prmparametro3.ParameterName = "idConexion";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro3.Value = conectionId;

                Prmparametro4.ParameterName = "aceptaFactura";
                Prmparametro4.SqlDbType = System.Data.SqlDbType.Bit;
                Prmparametro4.Value = emailInvoices;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "INS_USUARIO";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);
                Command.Parameters.Add(Prmparametro4);

                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;

            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }
            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"Register() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Gets the user by email and associate identifier.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="associateId">The associate identifier.</param>
        /// <returns></returns>
        public IBaseDALResponse GetUserCountByEmail(string email)
        {
            _log.Info("GetUserByEmailAndAssociateId() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            Prmparametro1.ParameterName = "IDSOCIO";
            Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro1.Value = -1;

            SqlParameter Prmparametro2 = new SqlParameter();
            Prmparametro2.ParameterName = "EMAIL";
            Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
            Prmparametro2.Value = email;

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SEL_USUARIO_IDSOCIO_EMAIL";
            Command.Parameters.Add(Prmparametro1);
            Command.Parameters.Add(Prmparametro2);

            try
            {
                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }

            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Info($"GetUserByEmailAndAssociateId() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Puts a new relationship
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="associateId"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public IBaseDALResponse PutRelacion(int userId, int associateId, int connectionId)
        {
            _log.Info("PutRelacion() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            Prmparametro1.ParameterName = "idUsuario";
            Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro1.Value = userId;

            Prmparametro2.ParameterName = "idSocio";
            Prmparametro2.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro2.Value = associateId;

            Prmparametro3.ParameterName = "idConexion";
            Prmparametro3.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro3.Value = connectionId;

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "INS_SOCIOS_CONEXIONES";
            Command.Parameters.Add(Prmparametro1);
            Command.Parameters.Add(Prmparametro2);
            Command.Parameters.Add(Prmparametro3);


            try
            {
                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }

            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Info($"PutRelacion() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="sendInvoiceEmail">if set to <c>true</c> [send invoice email].</param>
        /// <returns></returns>
        public IBaseDALResponse UpdateUserData(int userId, string newPassword, bool sendInvoiceEmail)
        {
            _log.Info("UpdateUserData() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            Prmparametro1.ParameterName = "idUsuario";
            Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro1.Value = userId;

            Prmparametro2.ParameterName = "passWord";
            Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
            Prmparametro2.Value = newPassword;

            Prmparametro3.ParameterName = "enviaFacturaMail";
            Prmparametro3.SqlDbType = System.Data.SqlDbType.Bit;
            Prmparametro3.Value = sendInvoiceEmail;

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "UPD_USUARIO_CONTRASENA";
            Command.Parameters.Add(Prmparametro1);
            Command.Parameters.Add(Prmparametro2);
            Command.Parameters.Add(Prmparametro3);


            try
            {
                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }

            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Info($"UpdateUserData() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Retrieves the user by email and associateId.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="sendInvoiceEmail">if set to <c>true</c> [send invoice email].</param>
        /// <returns></returns>
        public IBaseDALResponse GetUserByEmailAndAssociateId(string email, int associateId)
        {
            _log.Info("GetUserByEmailAndAssociateId() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();

            Prmparametro1.ParameterName = "XmlSocio";
            Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
            Prmparametro1.Value = associateId;

            Prmparametro2.ParameterName = "userEmail";
            Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
            Prmparametro2.Value = email;

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SEL_USUARIO_REESTABLECER";
            Command.Parameters.Add(Prmparametro1);
            Command.Parameters.Add(Prmparametro2);
  

            try
            {
                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }

            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Info($"UpdateUserData() Fin.");
            }

            return response;
        }

        
        #endregion
    }
}
