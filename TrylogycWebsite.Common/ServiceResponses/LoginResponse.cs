using System.Runtime.Serialization;
using CommonTrylogycWebsite.Models;

namespace CommonTrylogycWebsite.ServiceResponses
{

    /// <summary>
    /// Login Response class.
    /// </summary>
    
    public class LoginResponse : BaseResponse
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the dto user.
        /// </summary>
        /// <value>
        /// The dto user.
        /// </value>
        
        public WcfUser User { get; set; }

        #endregion
    }
}