namespace TrylogycWebsite.Common.ServiceRequests.Interfaces
{
    public interface IUpdateStatusPaymentRequest
    {
        #region Propiedades
        string transaccionPlataformaId { get; set; }
        int idPago { get; set; }
        int estadoPago { get; set; }
        #endregion
    }
}
