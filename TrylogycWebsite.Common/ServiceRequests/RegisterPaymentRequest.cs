using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{
    public class RegisterPaymentRequest : BaseRequest, IRegisterPaymentRequest
    {
        #region Property
        public string nroFactura { get; set; }
        public decimal importe { get; set; }
        //public string preference { get; set; }
        public int idSocio { get; set; }
        public int idConexion { get; set; }
        public int idMedioPago { get; set; }
        #endregion

        #region Métodos
        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
