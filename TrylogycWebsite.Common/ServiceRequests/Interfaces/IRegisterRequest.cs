namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{
    public interface IRegisterRequest
    {
        #region Properties
        string CGP { get; set; }
        string Code { get; set; }
        string Email { get; set; }
        string EmailConfirm { get; set; }
        bool EmailInvoices { get; set; }
        #endregion

    }
}