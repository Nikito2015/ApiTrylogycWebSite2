

namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{

    /// <summary>
    /// Interface for a login request.
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.Interfaces.IBaseRequest" />
    public interface ILoginRequest : IBaseRequest
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        #endregion
    }
}