using BLLTrylogycWebsite.Interfaces;
using BLLTrylogycWebsite.Responses.Interfaces;
using CommonTrylogycWebsite.DTO;
using CommonTrylogycWebsite.DTO.Interfaces;
using System.Collections.Generic;

namespace BLLTrylogycWebsite.Models.Interfaces
{
    public interface IBLLPago : IBLLBase
    {
        #region Methods
        /// <summary>
        /// RegisterPayment.
        /// </summary>
        /// <param name="nroFactura"></param>
        /// <param name="importe"></param>
        /// <param name="idSocio"></param>
        /// <param name="idConexion"></param>
        /// <param name="idMedioPago"></param>
        /// <returns></returns>
        IBLLResponseBase<int> RegisterPayment(string nroFactura, decimal importe, int idSocio, int idConexion, int idMedioPago);
        /// <summary>
        /// UpdatePayment.
        /// </summary>
        /// <param name="idPlataforma"></param>
        /// <param name="preference"></param>
        /// <param name="TransaccionComercioId"></param>
        /// <returns></returns>
        IBLLResponseBase<bool> UpdatePayment(int idPlataforma, string preference, string TransaccionComercioId);
        /// <summary>
        /// UpdateStatusPayment.
        /// </summary>
        /// <param name="idPago"></param>
        /// <param name="transaccionPlataformaId"></param>
        /// <param name="estadoPago"></param>
        /// <returns></returns>
        IBLLResponseBase<bool> UpdateStatusPayment(int idPago, string transaccionPlataformaId, int estadoPago);
        #endregion
    }
}
