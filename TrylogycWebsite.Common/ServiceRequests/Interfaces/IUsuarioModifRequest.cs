namespace TrylogycWebsite.Common.ServiceRequests.Interfaces
{
    public interface IUsuarioModifRequest
    {
        #region Propiedades
        string UserName { get; set; }
        string OldPassword { get; set; }
        string NewPassword { get; set; }
        string NewPasswordConfirm { get; set; }
        bool SendInvoiceEmail { get; set; }

        #endregion
    }
}