using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{

    /// <summary>
    /// A login request.
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.Interfaces.ILoginRequest" />
    
    public class LoginRequest : BaseRequest, ILoginRequest
    {

        #region Public Properties


        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        
        public string Password { get; set; }


        #endregion

        #region Public Methdos

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool IsValid()
        {
            return (!string.IsNullOrEmpty(UserName) ||
                        !string.IsNullOrEmpty(Password));
        }
        #endregion
    }
}