using System.Collections.Generic;
using System.Runtime.Serialization;
using CommonTrylogycWebsite.Models;

namespace CommonTrylogycWebsite.ServiceResponses
{
    
    public class UserAssociatesResponse : BaseResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>
        
        public List<WcfAssociate> Associates { get; set; }

        #endregion
    }
}