using System.Net;
using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceResponses.Interfaces;

namespace CommonTrylogycWebsite.ServiceResponses
{

    /// <summary>
    /// Base Response
    /// </summary>
    public abstract class BaseResponse : IBaseResponse
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets the response MSG.
        /// </summary>
        /// <value>
        /// The response MSG.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; } = (int)HttpStatusCode.PreconditionFailed;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the success respone.
        /// </summary>
        /// <param name="message">The message.</param>
        public IBaseResponse SetSuccessResponse(string message)
        {
            this.StatusCode = (int)HttpStatusCode.OK;
            this.Message = $"OK!. {message}";
            return this;
        }

        /// <summary>
        /// Sets the fail response.
        /// </summary>
        /// <param name="message">The message.</param>
        public IBaseResponse SetErrorResponse(string message)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
            this.Message = $"Error! {message}";
            return this;
        }

        /// <summary>
        /// Sets the unauthorized response.
        /// </summary>
        /// <param name="message">The message.</param>
        public IBaseResponse SetUnauthorizedResponse(string message)
        {
            this.StatusCode = (int)HttpStatusCode.Unauthorized;
            this.Message = $"No autorizado. {message}";
            return this;
        }

        /// <summary>
        /// Sets the bad request response.
        /// </summary>
        /// <param name="message">The message.</param>
        public IBaseResponse SetBadRequestResponse(string message)
        {
            this.StatusCode = (int)HttpStatusCode.BadRequest;
            this.Message = $"Error!. {message}";
            return this;
        }

        #endregion
    }
}