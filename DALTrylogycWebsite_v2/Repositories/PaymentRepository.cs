using System;
using System.Data.SqlClient;
using System.Data;
using DALTrylogycWebsite.DALResponses;
using DALTrylogycWebsite.DALResponses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using log4net;

namespace DataAccess.Repositories
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public PaymentRepository(ILog log, string connString) : base(log, connString)
        {

        }
        #endregion

        #region Enum
        public enum EstadosPagos
        {
            Creado = 0,
            Aprobado = 1,
            Rechazado = 2,
            Demorado = 3
        }
        #endregion

        #region Method
        /// <summary>
        /// Registers Payments.
        /// </summary>
        /// <param name="nroFactura">The nroFactura.</param>
        /// <param name="importe">The importe.</param>
        /// <param name="idSocio">The idSocio.</param>
        /// <param name="idConexion">The idConexion.</param>
        /// <param name="idMedioPago">The idMedioPago.</param>
        /// <returns></returns>
        public IBaseDALResponse RegisterPayment(string nroFactura, decimal importe, int idSocio, int idConexion, int idMedioPago)
        {
            _log.Info("Register() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();
            SqlParameter Prmparametro4 = new SqlParameter();
            SqlParameter Prmparametro5 = new SqlParameter();
            SqlParameter Prmparametro6 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "estado";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Bit;
                Prmparametro1.Value = EstadosPagos.Creado;

                Prmparametro2.ParameterName = "numeroFactura";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = nroFactura;

                Prmparametro3.ParameterName = "importe";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.Decimal;
                Prmparametro3.Value = importe;

                Prmparametro4.ParameterName = "idSocio";
                Prmparametro4.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro4.Value = idSocio;

                Prmparametro5.ParameterName = "idConexion";
                Prmparametro5.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro5.Value = idConexion;

                Prmparametro6.ParameterName = "idMedioPago";
                Prmparametro6.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro6.Value = idMedioPago;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "INS_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);
                Command.Parameters.Add(Prmparametro4);
                Command.Parameters.Add(Prmparametro5);
                Command.Parameters.Add(Prmparametro6);

                Connection.Open();
                //int insertedID = Convert.ToInt32(Command.ExecuteScalar());
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
        /// Updates Payments.
        /// </summary>
        /// <param name="idPlataforma">The idPlataforma.</param>
        /// <param name="preference">The preference.</param>
        /// <returns></returns>
        public IBaseDALResponse UpdatePayment(int idPlataforma, string preference, string TransaccionComercioId)
        {
            _log.Info("Update() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "idPago";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro1.Value = idPlataforma;

                Prmparametro2.ParameterName = "preference";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = preference;

                Prmparametro3.ParameterName = "TransaccionComercioId";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro3.Value = TransaccionComercioId;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "UPD_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);

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
                _log.Error($"Update() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Updates Status Payments.
        /// </summary>
        /// <param name="idPago"></param>
        /// <param name="transaccionPlataformaId"></param>
        /// <param name="estadoPago"></param>
        /// <returns></returns>
        public IBaseDALResponse UpdateStatusPayment(int idPago, string transaccionPlataformaId, int estadoPago)
        {
            _log.Info("Update() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "idPago";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro1.Value = idPago;

                Prmparametro2.ParameterName = "transaccionPlataformaId";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = transaccionPlataformaId;

                Prmparametro3.ParameterName = "estadoPago";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro3.Value = estadoPago;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "UPD_STATUS_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);

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
                _log.Error($"Update() Fin.");
            }

            return response;
        }
        #endregion
    }
}

