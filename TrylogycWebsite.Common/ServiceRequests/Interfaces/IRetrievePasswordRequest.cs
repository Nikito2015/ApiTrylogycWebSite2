namespace TrylogycWebsite.Common.ServiceRequests.Interfaces
{
    public interface IRetrievePasswordRequest
    {
        #region Propiedades
        string Email { get; set; }
        string CGP { get; set; }
        #endregion
    }
}
