namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{

    /// <summary>
    /// Interface for an add relation request
    /// </summary>
    public interface IAddRelationRequest
    {
        #region Properties
        string AssociateCode { get; set; }
        string CGP { get; set; }

        int UserId { get; set; }
        #endregion

        #region Methods
        bool IsValid();
        #endregion
    }
}