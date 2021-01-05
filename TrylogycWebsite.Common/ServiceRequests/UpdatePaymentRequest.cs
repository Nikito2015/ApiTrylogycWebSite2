using CommonTrylogycWebsite.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;
using TrylogycWebsite.Common.ServiceRequests.Interfaces;

namespace TrylogycWebsite.Common.ServiceRequests
{
    public class UpdatePaymentRequest : BaseRequest, IUpdatePaymentRequest
    {
        #region Propiedades
        public int idPlataforma { get; set; }
        public string preference { get; set; }
        public string TransaccionComercioId { get; set; }

        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
