using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO.Interfaces
{

    /// <summary>
    /// Interface for a DTOAssociateConnection
    /// </summary>
    [JsonConverter(typeof(DTOAssociateConnection))]
    public interface IDTOAssociateConnection
    {
        #region Properties

        /// <summary>
        /// Gets or sets the associate identifier.
        /// </summary>
        /// <value>
        /// The associate identifier.
        /// </value>
        int AssociateId { get; set; }

        /// <summary>
        /// Gets or sets the connection identifier.
        /// </summary>
        /// <value>
        /// The connection identifier.
        /// </value>
        int ConnectionId { get; set; }

        #endregion
    }
}
