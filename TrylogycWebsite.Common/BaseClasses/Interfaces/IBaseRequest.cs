namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{

    /// <summary>
    /// Interface for a base request
    /// </summary>
    public interface IBaseRequest
    {


        #region Methods

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid();
        #endregion
    }
}