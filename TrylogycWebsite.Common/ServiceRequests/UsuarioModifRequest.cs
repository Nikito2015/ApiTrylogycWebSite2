using CommonTrylogycWebsite.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;
using TrylogycWebsite.Common.ServiceRequests.Interfaces;

namespace TrylogycWebsite.Common.ServiceRequests
{
    public class UsuarioModifRequest : BaseRequest, IUsuarioModifRequest
    {
        #region Propiedades
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
        public bool SendInvoiceEmail { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
                return false;
           if (!NewPassword.Equals(NewPasswordConfirm))
                return false;
           if (NewPassword.Length <= 5)
                return false;
            return true;
        }
        #endregion
    }
}
