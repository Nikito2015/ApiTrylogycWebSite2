namespace TrylogycWebsite.Common.ServiceRequests.Interfaces
{
    public interface IUpdatePaymentRequest
    {
        #region Propiedades
        int idPlataforma { get; set; }
        string preference { get; set; }
        string TransaccionComercioId { get; set; }
        #endregion
    }
}
