using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO
{

    /// <summary>
    /// Class that implements the IDTOAssociateConnection
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.DTO.Interfaces.IDTOAssociateConnection" />
    public class DTOAssociateConnection : IDTOAssociateConnection
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets the associate identifier.
        /// </summary>
        /// <value>
        /// The associate identifier.
        /// </value>
        public int AssociateId { get; set; }

        /// <summary>
        /// Gets or sets the connection identifier.
        /// </summary>
        /// <value>
        /// The connection identifier.
        /// </value>
        public int ConnectionId { get; set; }

        #endregion

    }
}
