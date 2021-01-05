using System.Collections.Generic;
using System.Runtime.Serialization;
using CommonTrylogycWebsite.Models;

namespace CommonTrylogycWebsite.ServiceResponses
{
    
    public class UserBalancesResponse : BaseResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the balances.
        /// </summary>
        /// <value>
        /// The balances.
        /// </value>
        
        public List<WcfBalance> Balances { get; set; }

        #endregion
    }
}