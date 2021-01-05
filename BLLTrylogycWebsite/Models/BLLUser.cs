using BLLTrylogycWebsite.Enums;
using BLLTrylogycWebsite.Models.Interfaces;
using BLLTrylogycWebsite.Responses;
using BLLTrylogycWebsite.Responses.Interfaces;
using CommonTrylogycWebsite.DTO;
using CommonTrylogycWebsite.DTO.Extensions;
using CommonTrylogycWebsite.DTO.Interfaces;
using DALTrylogycWebsite.DALResponses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using DataAccess.Repositories;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml;

namespace BLLTrylogycWebsite.Models
{
    public class BLLUser : BLLBase, IBLLUser
    {

        #region Private Members
        private IUserRepository _userRepository;

        #endregion
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLLUser"/> class.
        /// </summary>
        /// <param name="log"></param>
        public BLLUser(ILog log, string connString, IConfiguration configuration) : base(log, connString, configuration)
        {
            _userRepository = new UserRepository(log, connString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IBLLResponseBase<IDTOUser> Login(string userEmail, string password, bool applyConnectionFiltering)
        {
            _log.Info("GetUserByName() Comienzo...");
            var bllUserResponse = new BLLResponseBase<IDTOUser>();
            try
            {
                _log.Info($"Recuperando usuario {userEmail}.");
                var user = _userRepository.GetUserByEmail(userEmail);

                if (user.Succeeded)
                {
                    if (user.Results.Tables?[0]?.Rows?.Count > 0)
                    {
                        _log.Info("Usuario recuperado.");
                        _log.Info("Convirtiendo dataset a DTO.");
                        var userRow = user.Results.Tables[0].Rows[0];
                        if (userRow["UserPass"]?.Equals(password) ?? false)
                        {
                            bllUserResponse.DTOResult = DTOUser.CreateFromDataRow(userRow);

                            _log.Info("Conversión a DTO exitosa.");
                            //Recuperar Conexiones
                            var bllConnectionsResponse = GetConnectionIdsFromUserId(bllUserResponse.DTOResult.Id);
                            if (bllConnectionsResponse.Status.Equals(Status.Success))
                            {
                                bllUserResponse.DTOResult.AssociatesConnections = bllConnectionsResponse.DTOResult;
                            }
                            else
                            {
                                _log.Error($"Error al recuperar las conexiones del usuario {userEmail}");
                                bllUserResponse.Status = Enums.Status.Fail;
                                bllUserResponse.Message = "Error al recuperar las conexiones del usuario.";
                            }

                            //Recuperar path de la dll
                            _log.Info($"Recuperando path de la dll en ejecución.");
                            var assemblyPath = GetExecutingAssemblyPath();

                            _log.Info($"Estableciendo filtro para conexiones.");
                            //Armar filtro para conexiones
                            var connectionsFilter = GetConnectionsFilter(bllUserResponse.DTOResult.AssociatesConnections, applyConnectionFiltering);

                            _log.Info($"Recuperando lista de saldos.");
                            //Popular lista de saldos.
                            bllUserResponse.DTOResult.AddBalancesFromDataTable(GetBalancesDataTableFromXml(connectionsFilter, assemblyPath));

                            _log.Info($"Recuperando lista de socios.");
                            //Popular lista de socios.
                            bllUserResponse.DTOResult.AddAssociatesFromDataTable(GetAssociatesDataTableFromXml(connectionsFilter, assemblyPath));
                            bllUserResponse.Status = Status.Success;
                        }
                        else
                        {
                            _log.Info("Contraseña errónea");
                            bllUserResponse.Status = Enums.Status.Fail;
                            bllUserResponse.Message = "Datos de acceso erróneos.";
                        }
                    }

                    else
                    {
                        _log.Info("Usuario no encontrado.");
                        bllUserResponse.Status = Enums.Status.Fail;
                        bllUserResponse.Message = "Usuario no encontrado.";
                    }
                }
                else
                {
                    var message = user.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                    _log.Info($"No se pudo recuperar el usuario {userEmail}. Motivo: {message} ");
                    bllUserResponse.Status = Enums.Status.Fail;
                    bllUserResponse.Message = "Ocurrieron Errores al recuperar el usuario.";
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrió una excepción durante la recuperación del usuario {userEmail}. Error: {ex.Message}");
                bllUserResponse.Status = Enums.Status.Fail;
                bllUserResponse.Message = $"Ocurrieron Errores al recuperar el usuario {userEmail}.";
            }
            return bllUserResponse;
        }

        /// <summary>
        /// Gets the connection ids from user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IBLLResponseBase<IEnumerable<IDTOAssociateConnection>> GetConnectionIdsFromUserId(int userId)
        {
            _log.Info("GetConnectionIdsFromUserId() Comienzo...");
            IBLLResponseBase<IEnumerable<IDTOAssociateConnection>> bllResponse = new BLLResponseBase<IEnumerable<IDTOAssociateConnection>>();
            List<DTOAssociateConnection> associateConnections = new List<DTOAssociateConnection>();
            try
            {
                _log.Info($"Recuperando conexiones del usuario con id {userId}.");
                var connections = _userRepository.GetConnectionsByUserId(userId);

                if (connections.Succeeded)
                {
                    _log.Info("Conexiones recuperadas.");
                    foreach (DataRow dr in connections.Results.Tables?[0]?.Rows)
                    {
                        associateConnections.Add(new DTOAssociateConnection()
                        {
                            AssociateId = Convert.ToInt32(dr["idSocio"]),
                            ConnectionId = Convert.ToInt32(dr["idConexion"]),
                        });
                    }
                    bllResponse.DTOResult = associateConnections;
                    bllResponse.Status = Status.Success;
                }

                else
                {
                    var message = connections.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                    _log.Info($"No se pudo recuperar el usuario con id {userId}. Motivo: {message} ");
                    bllResponse.Status = Enums.Status.Fail;
                    bllResponse.Message = message ?? "Ocurrieron Errores al recuperar las conexiones.";
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrió una excepción durante la recuperación de las conexiones del usuario id {userId}. Error: {ex.Message}");
                bllResponse.Status = Enums.Status.Fail;
                bllResponse.Message = $"Ocurrieron Errores al recuperar las conexiones del usuario id {userId}.";
            }
            return bllResponse;
        }

        /// <summary>
        /// Gets the invoice PDF in a base64 string format.
        /// </summary>
        /// <param name="associateId">The associate identifier.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <param name="retrieveFromFtp">if set to <c>true</c> [retrieve from FTP].</param>
        /// <returns></returns>
        public IBLLResponseBase<string> GetInvoicePdf(int associateId, int connectionId, string invoiceNumber, bool retrieveFromFtp)
        {
            _log.Info($"GetInvoicePdf() Comienzo.");
            IBLLResponseBase<string> pdfResponse = new BLLResponseBase<string>();
            try
            {
                if ((!System.IO.Directory.Exists(Path.Combine(GetExecutingAssemblyPath(), "Temp"))))
                    System.IO.Directory.CreateDirectory(Path.Combine(GetExecutingAssemblyPath(), "Temp"));

                string pdfFileBytes = null;
                string filePathAndName = null;

                if (retrieveFromFtp == true)
                {
                    _log.Info($"Recuperando archivo pdf desde FTP.");
                    string ftpAddress = _configuration.GetSection("FtpAddress").Value;
                    _log.Info($"Host FTP: {ftpAddress}.");
                    string ftpUser = _configuration.GetSection("ftpUser").Value;
                    _log.Info($"Usuario FTP: {ftpUser}.");
                    string ftpPassword = _configuration.GetSection("ftpPassWord").Value; 
                    _log.Info($"Contraseña FTP: {ftpPassword}.");
                    string fullFtpAddress = ftpAddress + CreateFullInvoiceNumberFromAssociateAndConnection(associateId, connectionId, invoiceNumber) + ".pdf";
                    _log.Info($"Dirección completa FTP: {fullFtpAddress}.");

                    FtpWebRequest request =
                            (FtpWebRequest)WebRequest.Create(fullFtpAddress);
                    request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                    request.UsePassive = true;
                    request.Timeout = 60000;
                    request.Method = WebRequestMethods.Ftp.DownloadFile;


                    using (Stream ftpStream = request.GetResponse().GetResponseStream())
                    {
                        _log.Info($"Archivo recuperado desde FTP.");
                        filePathAndName = Path.Combine(GetExecutingAssemblyPath(), @"Temp", DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
                        using (Stream fileStream = File.Create(filePathAndName))
                        {
                            ftpStream.CopyTo(fileStream);
                        }
                    }
                    _log.Info($"Convirtiendo a Base 64.");
                    pdfFileBytes = Convert.ToBase64String(File.ReadAllBytes(filePathAndName));
                    File.Delete(filePathAndName);

                }
                else
                {
                    _log.Info($"Recuperando archivo desde servidor local.");
                    filePathAndName = Path.Combine(GetExecutingAssemblyPath(), @"PDF", CreateFullInvoiceNumberFromAssociateAndConnection(associateId, connectionId, invoiceNumber) + ".pdf");
                    _log.Info($"Directorio {filePathAndName}.");
                    _log.Info($"Convirtiendo a Base 64.");
                    pdfFileBytes = Convert.ToBase64String(File.ReadAllBytes(filePathAndName));
                }

                pdfResponse.DTOResult = pdfFileBytes;
                pdfResponse.Status = Enums.Status.Success;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar recuperar el archivo PDF. {ex.Message}");
                pdfResponse.Status = Enums.Status.Fail;
                pdfResponse.Message = $"Ocurrieron errores al intentar recuperar el archivo PDF.";
            }

            return pdfResponse;
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="emailConfirm"></param>
        /// <param name="associateCode"></param>
        /// <param name="CGP"></param>
        /// <param name="emailInvoices"></param>
        /// <returns></returns>
        public IBLLResponseBase<bool> Register(string email, string emailConfirm, string associateCode, string CGP, bool emailInvoices)
        {
            _log.Info($"Register() Comienzo.");
            IBLLResponseBase<bool> bllResponse = new BLLResponseBase<bool>();
            string message = null;
            try
            {
                if (ValidateRegistrationData(email, emailConfirm, associateCode, CGP, ref message))
                {
                    int associateId = RetrieveAssociateIdFromAssociateCode(associateCode);
                    int connectionId = RetrieveConnectionIdFromAssociateCode(associateCode);

                    if (!UserAlreadyExists(email))
                    {
                        if (IsAssociateCodeAndConnectionValid(associateId, connectionId))
                        {
                            _log.Info($"Registrando usuario.");
                            var register = _userRepository.Register(email, associateId, connectionId, emailInvoices);
                            if (register.Succeeded)
                            {
                                _log.Info($"Enviando mail de registro.");
                                if (SendRegistrationEmail(email, register.Results.Tables[0].Rows[0][1].ToString()))
                                {
                                    _log.Info($"Mail enviado exitosamente.");
                                    bllResponse.DTOResult = true;
                                    bllResponse.Status = Status.Success;
                                    bllResponse.Message = "Usuario Registrado Exitosamente";
                                }
                                else
                                {
                                    _log.Info($"El usuario se registro exitosamente pero no se pudo enviar el mail de regitro");
                                    bllResponse.DTOResult = true;
                                    bllResponse.Status = Status.Success;
                                    bllResponse.Message = "Su usuario se registro exitosamente pero no se pudo enviar el mail de regitro con su contraseña. Por favor comuníquese con administración para solicitarlo";
                                }
                            }

                            else
                            {
                                message = register.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                                _log.Info($"No se pudo registrar el usuario {email}. Motivo: {message} ");
                                bllResponse.Status = Enums.Status.Fail;
                                bllResponse.Message = message ?? "Ocurrieron Errores al registrar el usuario.";
                            }
                        }
                        else
                        {
                            message = $"Codigo de Cliente {associateCode} Inexistente.";
                            _log.Info($"No se pudo registrar el usuario {email}. Motivo: {message} ");
                            bllResponse.Status = Enums.Status.Fail;
                            bllResponse.Message = message ?? "Ocurrieron Errores al registrar el usuario.";
                        }

                    }
                    else
                    {
                        message = $"Ya existe un usuario registrado con el mail {email}.";
                        _log.Info($"No se pudo registrar el usuario {email}. Motivo: {message} ");
                        bllResponse.Status = Enums.Status.Fail;
                        bllResponse.Message = message ?? "Ocurrieron Errores al registrar el usuario.";
                    }

                }
                else
                {
                    _log.Info($"Los datos de registro proporcionados no son válidos. Motivo: {message} ");
                    bllResponse.Status = Enums.Status.Fail;
                    bllResponse.Message = message;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrió una excepción durante el registro del usuario {email}. Error: {ex.Message}");
                bllResponse.Status = Enums.Status.Fail;
                bllResponse.Message = $"Ocurrieron Errores al registrar el usuario id {email}.";
            }
            return bllResponse;
        }

        /// <summary>
        /// Adds the relation.
        /// </summary>
        /// <param name="associateId">The associate identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        public IBLLResponseBase<bool> AddRelation(int userId, string associateCode, string cgp)
        {
            _log.Info($"GetInvoicePdf() Comienzo.");
            IBLLResponseBase<bool> addRelationResponse = new BLLResponseBase<bool>();
            try
            {
                var xmlAssociates = GetAssociatesDataTableFromXml(null, GetExecutingAssemblyPath());
                var associateId = RetrieveAssociateIdFromAssociateCode(associateCode);
                var connectionId = RetrieveConnectionIdFromAssociateCode(associateCode);

                var associate = xmlAssociates?.AsEnumerable()?.Where(t => Convert.ToInt32(t["Socio"]) == associateId &&
                                                                            Convert.ToInt32(t["Conexion"]) == connectionId &&
                                                                                t["CGP"].ToString().Trim() == cgp.Trim());
                if (associate?.Any() ?? false)
                {
                    //PutRelacion
                    _log.Info($"Insertando Relación Socio {associateId} - Conexión {connectionId}.");
                    var connections = _userRepository.PutRelacion(userId, associateId, connectionId);

                    if (connections.Succeeded)
                    {
                        _log.Info("Relación creada exitosamente.");

                        addRelationResponse.DTOResult = true;
                        addRelationResponse.Status = Status.Success;
                    }

                    else
                    {
                        var message = connections.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                        _log.Info($"No se pudo crear la relación. Motivo: {message} ");
                        addRelationResponse.Status = Enums.Status.Fail;
                        addRelationResponse.Message = message ?? "Ocurrieron Errores al intentar crear la relación.";
                    }
                }
                else
                {
                    _log.Info($"No se pudo encontrar el socio {associateId} - conexión {connectionId} - CGP {cgp} en el xml de socios.");
                    addRelationResponse.DTOResult = false;
                    addRelationResponse.Status = Enums.Status.Fail;
                    addRelationResponse.Message = "Los datos ingresados son incorrectos";
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar agregar la relación. {ex.Message}");
                addRelationResponse.Status = Enums.Status.Fail;
                addRelationResponse.Message = $"Ocurrieron errores al intentar agregar la relación.";
            }

            return addRelationResponse;
        }

        /// <summary>
        /// Retrieves the associates of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IBLLResponseBase<IEnumerable<IDTOAssociate>> GetUserAssociates(int userId)
        {
            _log.Info("GetUserAssociates() Comienzo...");
            var bllUserAssociatesResponse = new BLLResponseBase<IEnumerable<IDTOAssociate>>();
            IEnumerable<IDTOAssociateConnection> associateConnections = null;
            IEnumerable<IDTOAssociate> associates = null;

            try
            {

                //Recuperar Conexiones
                var bllConnectionsResponse = GetConnectionIdsFromUserId(userId);
                if (bllConnectionsResponse.Status.Equals(Status.Success))
                {
                    associateConnections = bllConnectionsResponse.DTOResult;
                    if (associateConnections?.Any() ?? false)
                    {
                        //Recuperar path de la dll
                        _log.Info($"Recuperando path de la dll en ejecución.");
                        var assemblyPath = GetExecutingAssemblyPath();

                        _log.Info($"Estableciendo filtro para conexiones.");
                        //Armar filtro para conexiones
                        var connectionsFilter = GetConnectionsFilter(associateConnections, true);

                        _log.Info($"Recuperando lista de socios.");
                        //Popular lista de socios.
                        bllUserAssociatesResponse.DTOResult = associates.PopulateAssociatesCollectionFromDataTable(GetAssociatesDataTableFromXml(connectionsFilter, assemblyPath));
                        bllUserAssociatesResponse.Status = Enums.Status.Success;
                    }
                    else
                    {
                        _log.Info($"El usuario {userId} no tiene conexiones vigentes.");
                        bllUserAssociatesResponse.DTOResult = null;
                        bllUserAssociatesResponse.Status = Enums.Status.Success;
                    }
                }
                else
                {
                    _log.Error($"Error al recuperar las conexiones del usuario con Id: {userId}");
                    bllConnectionsResponse.Status = Enums.Status.Fail;
                    bllConnectionsResponse.Message = "Error al recuperar las conexiones del usuario.";
                }

            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar recuperar los socios para el usuario {userId}. {ex.Message}");
                bllUserAssociatesResponse.Status = Enums.Status.Fail;
                bllUserAssociatesResponse.Message = $"Ocurrieron errores al intentar agregar la relación.";
            }
            return bllUserAssociatesResponse;

        }

        /// <summary>
        /// Retrieves the user balances.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IBLLResponseBase<IEnumerable<IDTOBalance>> GetUserBalances(int userId)
        {
            _log.Info("GetUserAssociates() Comienzo...");
            var bllUserBalancesResponse = new BLLResponseBase<IEnumerable<IDTOBalance>>();
            IEnumerable<IDTOAssociateConnection> associateConnections = null;
            IEnumerable<IDTOBalance> balances = null;

            try
            {
                //Recuperar Conexiones
                var bllConnectionsResponse = GetConnectionIdsFromUserId(userId);
                if (bllConnectionsResponse.Status.Equals(Status.Success))
                {
                    associateConnections = bllConnectionsResponse.DTOResult;
                    if (associateConnections?.Any() ?? false)
                    {
                        //Recuperar path de la dll
                        _log.Info($"Recuperando path de la dll en ejecución.");
                        var assemblyPath = GetExecutingAssemblyPath();

                        _log.Info($"Estableciendo filtro para conexiones.");
                        //Armar filtro para conexiones
                        var connectionsFilter = GetConnectionsFilter(associateConnections, true);

                        _log.Info($"Recuperando lista de saldos.");
                        //Popular lista de saldos.
                        bllUserBalancesResponse.DTOResult = balances.PopulateBalancesCollectionFromDataTable(GetBalancesDataTableFromXml(connectionsFilter, assemblyPath));
                        bllUserBalancesResponse.Status = Enums.Status.Success;
                    }
                    else
                    {
                        _log.Info($"El usuario {userId} no tiene conexiones vigentes.");
                        bllUserBalancesResponse.DTOResult = null;
                        bllUserBalancesResponse.Status = Enums.Status.Success;
                    }
                }
                else
                {
                    _log.Error($"Error al recuperar las conexiones del usuario con Id: {userId}");
                    bllConnectionsResponse.Status = Enums.Status.Fail;
                    bllConnectionsResponse.Message = "Error al recuperar las conexiones del usuario.";
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar recuperar las facturas para el usuario {userId}. {ex.Message}");
                bllUserBalancesResponse.Status = Enums.Status.Fail;
                bllUserBalancesResponse.Message = $"Ocurrieron errores al intentar agregar la relación.";

            }

            return bllUserBalancesResponse;

        }

        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public IBLLResponseBase<bool> UpdateUserData(string userName, string oldPassword, string newPassword, bool sendInvoiceMail)
        {
            _log.Info("UpdateUserData() Comienzo...");
            var bllUpdateUserResponse = new BLLResponseBase<bool>();
            try
            {
                _log.Info($"Recuperando usuario {userName}.");
                var user = _userRepository.GetUserByEmail(userName);
                if (user.Succeeded)
                {
                    if (user.Results.Tables?[0]?.Rows?.Count > 0)
                    {
                        _log.Info("Usuario recuperado.");
                        _log.Info("Convirtiendo dataset a DTO.");
                        var userRow = user.Results.Tables[0].Rows[0];
                        if (userRow["UserPass"]?.Equals(oldPassword) ?? false)
                        {
                            //El usuario existía y la contraseña es correcta --> Actualizamos la contraseña 
                            DTOUser dtoUser = DTOUser.CreateFromDataRow(userRow);
                            var updatedUserDalResponse = _userRepository.UpdateUserData(dtoUser.Id, newPassword, sendInvoiceMail);
                            if (updatedUserDalResponse.Succeeded)
                            {
                                bllUpdateUserResponse.Status = Status.Success;
                                bllUpdateUserResponse.Message = "Datos de usuario actualizados exitosamente";
                            }
                            else
                            {
                                var message = user.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                                _log.Info($"Ocurrieron errores al actualizar los datos del usuario {userName}. Motivo: {message} ");
                                bllUpdateUserResponse.Status = Enums.Status.Fail;
                                bllUpdateUserResponse.Message = "Ocurrieron Errores al actualizar los datos de usuario.";
                            }

                        }
                        else
                        {
                            _log.Info("Contraseña errónea");
                            bllUpdateUserResponse.Status = Enums.Status.Fail;
                            bllUpdateUserResponse.Message = "La contraseña actual proporcionada es incorrecta.";
                        }
                    }

                    else
                    {
                        _log.Info("Usuario no encontrado.");
                        bllUpdateUserResponse.Status = Enums.Status.Fail;
                        bllUpdateUserResponse.Message = "Usuario no encontrado.";
                    }
                }
                else
                {
                    var message = user.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                    _log.Info($"No se pudo recuperar el usuario {userName}. Motivo: {message} ");
                    bllUpdateUserResponse.Status = Enums.Status.Fail;
                    bllUpdateUserResponse.Message = "Ocurrieron Errores al recuperar el usuario.";
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar actualizar los datos del usuario {userName}. {ex.Message}");
                bllUpdateUserResponse.Status = Enums.Status.Fail;
                bllUpdateUserResponse.Message = $"Ocurrieron errores al intentar actualizar los datos del usuario.";
            }
            return bllUpdateUserResponse;
        }

        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public IBLLResponseBase<bool> RetrievePassword(string email, string cgp)
        {
            _log.Info("RetrievePassword() Comienzo...");
            var bllUpdateUserResponse = new BLLResponseBase<bool>();
            try
            {
                int associateId = Convert.ToInt32(cgp.Substring(0, 6));
                var user = _userRepository.GetUserByEmailAndAssociateId(email, associateId);
                if (user.Succeeded)
                {
                    if (user.Results.Tables?[0]?.Rows?.Count > 0)
                    {
                        _log.Info("Usuario recuperado.");
                        _log.Info("Convirtiendo dataset a DTO.");
                        var userRow = user.Results.Tables[0].Rows[0];

                        //El usuario existía --> Enviamos el password por mail.
                        SendRegistrationEmail(userRow["userEmail"].ToString(), userRow["userPass"].ToString());
                        bllUpdateUserResponse.Status = Enums.Status.Success;
                        bllUpdateUserResponse.Message = "Enviamos un email a su casilla con los datos de acceso.";
                    }

                    else
                    {
                        _log.Info("Usuario no encontrado.");
                        bllUpdateUserResponse.Status = Enums.Status.Fail;
                        bllUpdateUserResponse.Message = "Usuario no encontrado.";
                    }
                }
                else
                {
                    var message = user.Results?.Tables?[0].Rows?[0]?[1]?.ToString();
                    _log.Info($"No se pudo recuperar el usuario {email}. Motivo: {message} ");
                    bllUpdateUserResponse.Status = Enums.Status.Fail;
                    bllUpdateUserResponse.Message = "Ocurrieron Errores al recuperar el usuario.";
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar reestablecer la contraseña del usuario {email}. {ex.Message}");
                bllUpdateUserResponse.Status = Enums.Status.Fail;
                bllUpdateUserResponse.Message = $"Ocurrieron errores al intentar reestablecer su contraseña.";
            }
            return bllUpdateUserResponse;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a conection filtering string
        /// </summary>
        /// <param name="associateConnections"></param>
        /// <param name="applyConnectionFiltering"></param>
        /// <returns></returns>
        private string GetConnectionsFilter(IEnumerable<IDTOAssociateConnection> associateConnections, bool applyConnectionFiltering)
        {
            _log.Info($"GetConnectionsFilter() Comienzo.");
            var connectionsFilter = string.Empty;
            if (applyConnectionFiltering &&
                    associateConnections != null &&
                        associateConnections.Any())
            {
                foreach (var ac in associateConnections)
                {
                    connectionsFilter += "(SOCIO = " + ac.AssociateId + " AND CONEXION = " + ac.ConnectionId + ") OR ";
                }
            }
            if (!connectionsFilter.Equals(string.Empty))
                connectionsFilter = connectionsFilter.Substring(0, connectionsFilter.Length - 4);

            return connectionsFilter;
        }

        /// <summary>
        /// Retrieves the balances table from xml file
        /// </summary>
        /// <returns></returns>
        private DataTable GetBalancesDataTableFromXml(string connnectionsFilter, string assemblyFolder)
        {
            _log.Info($"GetBalancesDataTableFromXml() Comienzo.");
            string xmlFileName = Path.Combine(assemblyFolder, @"XmlFiles", @"SALDOS.xml");

            XmlReader xmlFile = XmlReader.Create(xmlFileName, new XmlReaderSettings());
            var dvSaldos = GetDataViewFromXml(xmlFile);
            dvSaldos.RowFilter = connnectionsFilter;
            return dvSaldos.ToTable();
        }

        /// <summary>
        /// Retrieves the associates table from xml file
        /// </summary>
        /// <returns></returns>
        private DataTable GetAssociatesDataTableFromXml(string connnectionsFilter, string assemblyFolder)
        {
            _log.Info($"GetAssociatesDataTableFromXml() Comienzo.");
            string xmlFileName = Path.Combine(assemblyFolder, @"XmlFiles", @"SOCIOS.xml");

            XmlReader xmlFile = XmlReader.Create(xmlFileName, new XmlReaderSettings());
            var dvSocios = GetDataViewFromXml(xmlFile);
            dvSocios.RowFilter = connnectionsFilter;
            return dvSocios.ToTable();
        }

        /// <summary>
        /// Retrieves the executing assembly folder path
        /// </summary>
        /// <returns></returns>
        private string GetExecutingAssemblyPath()
        {
            _log.Info($"GetExecutingAssemblyPath() Comienzo.");
            string file = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            FileInfo fileInfo = new FileInfo(file);
            return fileInfo.DirectoryName;
        }

        /// <summary>
        /// Retrieves the DataView from an xml reader
        /// </summary>
        /// <param name="xmlFile"></param>
        private DataView GetDataViewFromXml(XmlReader xmlFile)
        {
            _log.Info($"GetDataViewFromXml() Comienzo.");
            DataSet ds2 = new DataSet();
            ds2.ReadXml(xmlFile);
            DataTable dtSaldos = ds2.Tables[0];
            return new DataView(dtSaldos);
        }

        /// <summary>
        /// Creates the full invoice number from associate and connection.
        /// </summary>
        /// <param name="associateId">The associate identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        private string CreateFullInvoiceNumberFromAssociateAndConnection(int associateId, int connectionId, string invoiceNumber)
        {
            _log.Info($"CreateFullInvoiceNumberFromAssociateAndConnection() Comienzo.");
            return associateId.ToString().PadLeft(6, '0') +
                     connectionId.ToString().PadLeft(4, '0') +
                         invoiceNumber.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Validates user registration data
        /// </summary>
        /// <param name="email"></param>
        /// <param name="emailConfirm"></param>
        /// <param name="associateCode"></param>
        /// <param name="CGP"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateRegistrationData(string email, string emailConfirm, string associateCode, string CGP, ref string message)
        {
            _log.Info($"ValidateRegistrationData() Comienzo.");

            if (!ValidateEmails(email, emailConfirm, ref message))
                return false;

            if (!ValidateAssociateId(associateCode, ref message))
                return false;

            return true;
        }

        /// <summary>
        /// Validates emails
        /// </summary>
        /// <param name="email1"></param>
        /// <param name="email2"></param>
        /// <returns></returns>
        private bool ValidateEmails(string email1, string email2, ref string message)
        {
            _log.Info($"ValidateEmails() Comienzo.");
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email1);
                if (!email1.Equals(email2))
                {
                    _log.Info($"Los emails ingresados no coinciden. {email1}, {email2}");
                    message = "Los emails ingresados no coinciden.";
                    return false;
                }
                return true;
            }
            catch
            {
                _log.Info($"El email proporcionado no tiene el formato correcto. {email1}");
                message = "El email proporcionado no tiene el formato correcto.";
                return false;
            }

            finally
            {
                _log.Info($"ValidateEmails() Fin.");
            }

        }

        /// <summary>
        /// Validates the associate Id.
        /// </summary>
        /// <param name="associateCode"></param>
        /// <returns></returns>
        private bool ValidateAssociateId(string associateCode, ref string message)
        {
            _log.Info($"ValidateAssociateId() Comienzo.");
            if (!associateCode?.Length.Equals(10) ?? true)
            {
                message = "Codigo de Cliente debe tener un tamaño de 10 caracteres.";
                return false;
            }

            _log.Info($"ValidateAssociateId() Fin.");
            return true;
        }

        /// <summary>
        /// Retrieves the AssociateId from the associate code.
        /// </summary>
        /// <param name="associateCode"></param>
        /// <returns></returns>
        private int RetrieveAssociateIdFromAssociateCode(string associateCode)
        {
            _log.Info($"RetrieveAssociateIdFromAssociateCode() Comienzo.");
            var associateId = associateCode.Substring(0, 6);
            _log.Info($"Código de socio calculado: {associateId}.");
            _log.Info($"RetrieveAssociateIdFromAssociateCode() Fin.");
            return Convert.ToInt32(associateId);
        }

        /// <summary>
        /// Retrieves the Connection Id from the associate code.
        /// </summary>
        /// <param name="associateCode"></param>
        /// <returns></returns>
        private int RetrieveConnectionIdFromAssociateCode(string associateCode)
        {
            _log.Info($"RetrieveConnectionIdFromAssociateCode() Comienzo.");
            var connectionId = associateCode.Substring(6, 4);
            _log.Info($"Código de socio calculado: {connectionId}.");
            _log.Info($"RetrieveConnectionIdFromAssociateCode() Fin.");
            return Convert.ToInt32(connectionId);
        }

        /// <summary>
        /// Checks if a user is already registered.
        /// </summary>
        /// <param name="associateId"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        private bool UserAlreadyExists(string email)
        {
            _log.Info($"UserAlreadyExists() Comienzo.");
            IBaseDALResponse user;
            try
            {
                _log.Info($"Verificando si usuario ya estaba registrado.");
                user = _userRepository.GetUserCountByEmail(email);
                if (user.Succeeded)
                {
                    var userCount = Convert.ToInt32(user.Results.Tables[0].Rows[0][0]);
                    if (userCount > 0)
                        return true;
                    else
                        return false;
                }

                else
                {
                    throw new Exception(user?.Results?.Tables[0]?.Rows[0][1]?.ToString());
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrió una excepción al intentar recuperar la cuenta de usuarcios con el email {email}. Error: {ex.Message}");
                throw ex;
            }
        }

        /// <summary>
        /// Validates the associate code.
        /// </summary>
        /// <param name="associateCode"></param>
        /// <returns></returns>
        private bool IsAssociateCodeAndConnectionValid(int associateId, int connectionId)
        {
            _log.Info($"IsAssociateCodeValid() Comienzo.");

            try
            {
                var associates = GetAssociatesDataTableFromXml(null, GetExecutingAssemblyPath());
                return associates?
                    .AsEnumerable()
                    .Where(r =>
                        r.Field<string>("Socio").Equals(associateId.ToString()) &&
                            r.Field<string>("Conexion").Equals(connectionId.ToString()))
                    .Any() ?? false;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrió una excepción al intentar recuperar el xml de socios.");
                throw ex;
            }
        }

        /// <summary>
        /// Sends the registration email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        private bool SendRegistrationEmail(string email, string password)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_configuration.GetSection("email").Value, _configuration.GetSection("site").Value);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Sus datos de Acceso al sitio " + _configuration.GetSection("site").Value;
                    message.IsBodyHtml = true;
                    message.Body = "<html><body><span style='font-family:Georgia;font-size:14px;font-style:normal;font-weight:normal;text-decoration:none;text-transform:none;color:000000;background-color:ffffff;'><p>" + "Nueva Consulta desde Mi sitio Web" + "<br/>Su contraseña para acceder al sitio: " + password + "<br/>" + "</p></span></body></html>";

                    using (var client = new SmtpClient())
                    {
                        client.Host = _configuration.GetSection("host").Value.ToString();
                        client.Port = Convert.ToInt32(_configuration.GetSection("port").Value);
                        client.UseDefaultCredentials = Convert.ToBoolean((_configuration.GetSection("usedefaultcredentials").Value));
                        client.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("email").Value, _configuration.GetSection("password").Value);
                        client.EnableSsl = Convert.ToBoolean(_configuration.GetSection("enablessl").Value);
                        client.Send(message);
                    }
                }
                return true;
            }

            catch (Exception ex)
            {
                //TODO: Si el envío de mail fracasara, el usuario nunca se enteraría y tampoco podría registrarse de nuevo. Que hacemos en esta instancia?
                _log.Error($"Ocurrió una excepción al intentar enviar el mail de registro al usuario {email}. Error: {ex.Message}");
                return false;
            }
        }

        #endregion

    }
}
