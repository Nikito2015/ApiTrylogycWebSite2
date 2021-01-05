using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO.Interfaces
{

    /// <summary>
    /// Interface for a DTOConnection
    /// </summary>
    public interface IDTOConnection
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier conexion.
        /// </summary>
        /// <value>
        /// The identifier conexion.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the XML socio.
        /// </summary>
        /// <value>
        /// The XML socio.
        /// </value>
        int XmlAssociate { get; set; }

        /// <summary>
        /// Gets or sets the XML conexion.
        /// </summary>
        /// <value>
        /// The XML conexion.
        /// </value>
        int XmlConnection { get; set; }

        /// <summary>
        /// Gets or sets the connection CGP.
        /// </summary>
        /// <value>
        /// The connection CGP.
        /// </value>
        string CGP { get; set; }

        /// <summary>
        /// Gets or sets the connection address.
        /// </summary>
        /// <value>
        /// The connection address.
        /// </value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the city or town.
        /// </summary>
        /// <value>
        /// The city or town.
        /// </value>
        string CityOrTown { get; set; }

        #endregion
    }
}
