namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{
    public interface IRegisterPaymentRequest
    {
        #region Properties
        string nroFactura { get; set; }
        decimal importe { get; set; }
        //string preference { get; set; }
        int idSocio { get; set; }
        int idConexion { get; set; }
        int idMedioPago { get; set; }
        #endregion
    }
}
