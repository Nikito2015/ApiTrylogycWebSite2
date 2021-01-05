using DALTrylogycWebsite.DALResponses.Interfaces;
using System.Data;

namespace DALTrylogycWebsite.DALResponses
{

    /// <summary>
    /// Class that represents a Base DAL Response.
    /// </summary>
    /// <seealso cref="DALTrylogycWebsite.DALResponses.Interfaces.IBaseDALResponse" />
    public class BaseDALResponse : IBaseDALResponse
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDALResponse"/> class.
        /// </summary>
        public BaseDALResponse()
        {
            Results = new DataSet();
        }

        #endregion
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IBaseDALResponse" /> is succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </value>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public DataSet Results { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fills the error response.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.Exception">Results dataset must not be null to Fill error response</exception>
        public void FillErrorResponse(int code, string message)
        {
            if (Results == null)
                throw new System.Exception("Results dataset must not be null to Fill error response");

            Results.Tables.Add();
            Results.Tables[0].TableName = "Error";
            Results.Tables[0].Rows.Add();
            Results.Tables[0].Columns.Add();
            Results.Tables[0].Columns.Add();
            Results.Tables[0].Rows[0][0] = code;
            Results.Tables[0].Rows[0][1] = message;
        }
        #endregion

    }
}
