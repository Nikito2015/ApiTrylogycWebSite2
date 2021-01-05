using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{

    /// <summary>
    /// UserAssociatesRequest class
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.BaseRequest" />
    
    public class UserAssociatesAndBalancesRequest : BaseRequest, IUserAssociatesAndBalancesRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        
        public int UserId { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}