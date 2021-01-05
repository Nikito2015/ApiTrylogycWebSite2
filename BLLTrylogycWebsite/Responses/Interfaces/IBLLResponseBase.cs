using BLLTrylogycWebsite.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTrylogycWebsite.Responses.Interfaces
{

    /// <summary>
    /// Interface for a base BLL Response.
    /// </summary>
    public interface IBLLResponseBase<T> 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        Status Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the dto result.
        /// </summary>
        /// <value>
        /// The dto result.
        /// </value>
        T DTOResult { get; set; }

        /// <summary>
        /// Gets or sets the dto results.
        /// </summary>
        /// <value>
        /// The dto results.
        /// </value>
        IEnumerable<T> DTOResults { get; set; }

        #endregion
    }
}
