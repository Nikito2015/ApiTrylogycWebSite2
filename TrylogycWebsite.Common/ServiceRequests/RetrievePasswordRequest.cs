using CommonTrylogycWebsite.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;
using TrylogycWebsite.Common.ServiceRequests.Interfaces;

namespace TrylogycWebsite.Common.ServiceRequests
{
    public class RetrievePasswordRequest : BaseRequest, IRetrievePasswordRequest
    {
        #region Propiedades
        public string Email { get; set; }
        public string CGP { get; set; }

        #endregion

        #region Métodos
        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Email))
                return false;
            if (string.IsNullOrEmpty(CGP))
                return false;

            return true;
        }
        #endregion
    }
}
