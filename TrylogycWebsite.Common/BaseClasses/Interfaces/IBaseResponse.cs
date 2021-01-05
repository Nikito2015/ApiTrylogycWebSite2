namespace CommonTrylogycWebsite.ServiceResponses.Interfaces
{
    /// <summary>
    /// Interface for a base response
    /// </summary>
    public interface IBaseResponse
    {
        #region Properties
        /// <summary>
        /// Gets or sets the response MSG.
        /// </summary>
        /// <value>
        /// The response MSG.
        /// </value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        int StatusCode { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the success respone.
        /// </summary>
        /// <param name="message">The message.</param>
        IBaseResponse SetSuccessResponse(string message);

        /// <summary>
        /// Sets the fail response.
        /// </summary>
        /// <param name="message">The message.</param>
        IBaseResponse SetErrorResponse(string message);

        /// <summary>
        /// Sets the unauthorized response.
        /// </summary>
        /// <param name="message">The message.</param>
        IBaseResponse SetUnauthorizedResponse(string message);

        /// <summary>
        /// Sets the bad request response.
        /// </summary>
        /// <param name="message">The message.</param>
        IBaseResponse SetBadRequestResponse(string message);

        #endregion
    }
}