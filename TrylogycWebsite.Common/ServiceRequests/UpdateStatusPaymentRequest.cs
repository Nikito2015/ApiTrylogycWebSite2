using CommonTrylogycWebsite.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;
using TrylogycWebsite.Common.ServiceRequests.Interfaces;

namespace TrylogycWebsite.Common.ServiceRequests
{
    public class UpdateStatusPaymentRequest : BaseRequest, IUpdateStatusPaymentRequest
    {
        #region Propiedades     
        public string transaccionPlataformaId { get; set; }
        public int idPago { get; set; }
        public int estadoPago { get; set; }

        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
