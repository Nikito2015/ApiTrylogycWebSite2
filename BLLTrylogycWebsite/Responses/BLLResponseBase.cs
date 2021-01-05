using BLLTrylogycWebsite.Enums;
using BLLTrylogycWebsite.Responses.Interfaces;
using System.Collections.Generic;

namespace BLLTrylogycWebsite.Responses
{

    /// <summary>
    /// Base BLL Response.
    /// </summary>
    public class BLLResponseBase<T> : IBLLResponseBase<T>  
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="BLLResponseBase{T}"/> class.
        /// </summary>
        public BLLResponseBase()
        {
            Status = Status.Fail;
        }
        #endregion
        #region Public Properties
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the dto result.
        /// </summary>
        /// <value>
        /// The dto result.
        /// </value>
        public T DTOResult { get; set; }

        /// <summary>
        /// Gets or sets the dto results.
        /// </summary>
        /// <value>
        /// The dto results.
        /// </value>
        public IEnumerable<T> DTOResults { get; set; }
        #endregion
    }
}
