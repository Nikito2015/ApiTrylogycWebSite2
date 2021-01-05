using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{

    
    /// <summary>
    /// Base Request
    /// </summary>
    public abstract class BaseRequest : IBaseRequest
    {

        #region Public Methods

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsValid();
        #endregion
    }
}