using System.Data;

namespace DALTrylogycWebsite.DALResponses.Interfaces
{

    /// <summary>
    /// Interface for a base dal response.
    /// </summary>
    public interface IBaseDALResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IBaseDALResponse"/> is succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </value>
        bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        DataSet Results { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Fills the error response.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        void FillErrorResponse(int code, string message);
        #endregion
    }
}
