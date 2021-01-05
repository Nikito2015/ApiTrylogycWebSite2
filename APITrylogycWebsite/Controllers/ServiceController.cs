using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLLTrylogycWebsite.Enums;
using BLLTrylogycWebsite.Models;
using BLLTrylogycWebsite.Models.Interfaces;
using CommonTrylogycWebsite.Models;
using CommonTrylogycWebsite.ServiceRequests;
using CommonTrylogycWebsite.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TrylogycWebsite.Common.ServiceRequests;
using TrylogycWebsite.Common.ServiceResponses;

namespace APITrylogycWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {


        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ServiceController));
        private string _connString;
        private IConfiguration _configuration;
        public ServiceController(IConfiguration configuration)
        {
            _connString = ConfigurationExtensions.GetConnectionString(configuration, "appConn");
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        [Route("Login")]
        public LoginResponse Login([FromBody] LoginRequest request)
        {
            _log.Info("Login() Comienzo...");
            var response = new LoginResponse();

            if (!request.IsValid())
                return (LoginResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                var bllResponse = bllUser.Login(request.Email, request.Password, true);
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.User = WcfUser.CreateFromDTO(bllResponse.DTOResult);
                    response.SetSuccessResponse(null);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("Login() Fin...");
            }
            return response;


        }

        [HttpPost]
        [Route("RecuperarPdfFactura")]
        public InvoicePdfResponse GetInvoicePdf(InvoicePdfRequest request)
        {
            _log.Info("GetInvoicePdf() Comienzo...");
            var response = new InvoicePdfResponse();

            if (!request.IsValid())
                return (InvoicePdfResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLL User.");
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                _log.Info("Recuperando Pdf como array de bytes.");
                var bllResponse = bllUser.GetInvoicePdf(request.AssociateId, request.ConnectionId, request.InvoiceNumber, request.RetrieveFromFTP);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.InvoicePdf = bllResponse.DTOResult;
                    response.SetSuccessResponse(null);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("GetInvoicePdf() Fin...");
            }


            return response;

        }

        [HttpPost]
        [Route("Register")]
        public RegisterResponse Register(RegisterRequest request)
        {
            _log.Info("GetInvoicePdf() Comienzo...");
            var response = new RegisterResponse();

            if (!request.IsValid())
                return (RegisterResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLL User.");
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                _log.Info($"Registrando usuario {request.Email}.");
                var bllResponse = bllUser.Register(request.Email, request.EmailConfirm, request.Code, request.CGP, request.EmailInvoices);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("Register() Fin...");
            }

            return response;
        }

        [HttpPost]
        [Route("AddRelation")]
        public AddRelationResponse AddRelation(AddRelationRequest request)
        {
            _log.Info("AddRelation() Comienzo...");
            var response = new AddRelationResponse();

            if (!request.IsValid())
                return (AddRelationResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLL User.");
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                _log.Info($"Registrando Relación.");
                var bllResponse = bllUser.AddRelation(request.UserId, request.AssociateCode, request.CGP);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }
            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("AddRelation() Fin...");
            }

            return response;
        }

        [HttpPost]
        [Route("GetUserAssociates")]
        public UserAssociatesResponse GetUserAssociates(UserAssociatesAndBalancesRequest request)
        {
            _log.Info("GetUserAssociates() Comienzo...");
            var response = new UserAssociatesResponse();

            if (!request.IsValid())
                return (UserAssociatesResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                var bllResponse = bllUser.GetUserAssociates(request.UserId);
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.Associates = WcfAssociate.CreateAssociatesCollectionFromDTO(bllResponse.DTOResult);
                    response.SetSuccessResponse(null);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("Login() Fin...");
            }
            return response;
        }

        [HttpPost]
        [Route("GetUserBalances")]
        public UserBalancesResponse GetUserBalances(UserAssociatesAndBalancesRequest request)
        {
            _log.Info("GetUserBalances() Comienzo...");
            var response = new UserBalancesResponse();

            if (!request.IsValid())
                return (UserBalancesResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                var bllResponse = bllUser.GetUserBalances(request.UserId);
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.Balances = WcfBalance.CreateBalancesCollectionFromDTO(bllResponse.DTOResult);
                    response.SetSuccessResponse(null);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("Login() Fin...");
            }
            return response;
        }

        [HttpPost]
        [Route("UpdateUserData")]
        public UsuarioModifResponse UpdateUserData(UsuarioModifRequest request)
        {
            _log.Info("UpdateUserData() Comienzo...");
            var response = new UsuarioModifResponse();

            if (!request.IsValid())
                return (UsuarioModifResponse)response.SetBadRequestResponse("Los datos ingresados son erróneos. La contraseña nueva y su confirmación deben coincidir y tener más de 5 caracteres.");

            try
            {
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                var bllResponse = bllUser.UpdateUserData(request.UserName,request.OldPassword,request.NewPassword, request.SendInvoiceEmail);
                if (bllResponse.Status.Equals(Status.Success))
                {     
                    response.SetSuccessResponse(bllResponse.Message);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("UpdateUserData() Fin...");
            }
            return response;
        }

        [HttpPost]
        [Route("RetrievePassword")]
        public RetrievePasswordResponse UpdateUserData(RetrievePasswordRequest request)
        {
            _log.Info("RetrievePassword() Comienzo...");
            var response = new RetrievePasswordResponse();

            if (!request.IsValid())
                return (RetrievePasswordResponse)response.SetBadRequestResponse("Los datos ingresados son erróneos. Debe ingresar su CGP y su Email correctamente.");

            try
            {
                IBLLUser bllUser = new BLLUser(_log, _connString,_configuration);
                var bllResponse = bllUser.RetrievePassword(request.Email, request.CGP);
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("RetrievePassword() Fin...");
            }
            return response;
        }

        [HttpPost]
        [Route("RegisterPayment")]
        public RegisterPaymentResponse RegisterPayment(RegisterPaymentRequest request)
        {
            _log.Info("RegisterPaymentResponse() Comienzo...");
            var response = new RegisterPaymentResponse();

            if (!request.IsValid())
                return (RegisterPaymentResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLLPago.");
                IBLLPago bllPago = new BLLPago(_log, _connString, _configuration);
                _log.Info($"Registrando Factura: {request.nroFactura}.");
                var bllResponse = bllPago.RegisterPayment(request.nroFactura, request.importe, request.idSocio, request.idConexion, request.idMedioPago);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                    response.idPlataforma = bllResponse.DTOResult;
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("RegisterPaymentResponse() Fin...");
            }

            return response;
        }
        [HttpPost]
        [Route("UpdatePayment")]
        public UpdatePaymentResponse UpdatePayment(UpdatePaymentRequest request)
        {
            _log.Info("UpdatePaymentResponse() Comienzo...");
            var response = new UpdatePaymentResponse();

            if (!request.IsValid())
                return (UpdatePaymentResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLLPago.");
                IBLLPago bllPago = new BLLPago(_log, _connString, _configuration);
                _log.Info($"Actualizando Pago: {request.idPlataforma}.");
                var bllResponse = bllPago.UpdatePayment(request.idPlataforma, request.preference, request.TransaccionComercioId);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                    response.pagoActualizado = true;
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                    response.pagoActualizado = false;
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("UpdatePaymentResponse() Fin...");
            }

            return response;
        }
        [HttpPost]
        [Route("UpdateStatusPayment")]
        public UpdateStatusPaymentResponse UpdateStatusPayment(UpdateStatusPaymentRequest request)
        {
            _log.Info("UpdateStatusPaymentResponse() Comienzo...");
            var response = new UpdateStatusPaymentResponse();

            if (!request.IsValid())
                return (UpdateStatusPaymentResponse)response.SetBadRequestResponse("Los datos proporcionados no son válidos.");

            try
            {
                _log.Info("Inicializando BLLPago.");
                IBLLPago bllPago = new BLLPago(_log, _connString, _configuration);
                _log.Info($"Actualizando Pago: {request.idPago}.");
                var bllResponse = bllPago.UpdateStatusPayment(request.idPago, request.transaccionPlataformaId, request.estadoPago);
                _log.Info($"Respuesta de la capa de negocios: {bllResponse?.Status.ToString()}. Mensaje {bllResponse?.Message}");
                if (bllResponse.Status.Equals(Status.Success))
                {
                    response.SetSuccessResponse(bllResponse.Message);
                    response.pagoActualizado = true;
                }
                else
                {
                    response.SetBadRequestResponse(bllResponse.Message);
                    response.pagoActualizado = false;
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores: {ex}");
                response.SetErrorResponse(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            finally
            {
                _log.Info("UpdateStatusPaymentResponse() Fin...");
            }

            return response;
        }

    }
}