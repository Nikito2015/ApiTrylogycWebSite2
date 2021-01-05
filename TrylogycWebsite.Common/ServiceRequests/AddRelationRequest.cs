using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{

    /// <summary>
    /// Class for an add relation request
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.BaseRequest" />
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.Interfaces.IInvoicePdfRequest" />
    public class AddRelationRequest : BaseRequest, IAddRelationRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the associate identifier.
        /// </summary>
        /// <value>
        /// The associate identifier.
        /// </value>
        
        public string AssociateCode { get; set; }

        /// <summary>
        /// Gets or sets the connection identifier.
        /// </summary>
        /// <value>
        /// The connection identifier.
        /// </value>
        
        public string CGP { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        
        public int UserId { get; set; }

        #endregion

        #region Public Methdos

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return !(string.IsNullOrEmpty(CGP) ||
                        string.IsNullOrEmpty(AssociateCode));
        }
        #endregion
    }
}