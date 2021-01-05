using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO
{

    public class DTOConnection : IDTOConnection
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier conexion.
        /// </summary>
        /// <value>
        /// The identifier conexion.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the XML socio.
        /// </summary>
        /// <value>
        /// The XML socio.
        /// </value>
        public int XmlAssociate { get; set; }

        /// <summary>
        /// Gets or sets the XML conexion.
        /// </summary>
        /// <value>
        /// The XML conexion.
        /// </value>
        public int XmlConnection { get; set; }

        /// <summary>
        /// Gets or sets the connection CGP.
        /// </summary>
        /// <value>
        /// The connection CGP.
        /// </value>
        public string CGP { get; set; }

        /// <summary>
        /// Gets or sets the connection address.
        /// </summary>
        /// <value>
        /// The connection address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city or town.
        /// </summary>
        /// <value>
        /// The city or town.
        /// </value>
        public string CityOrTown { get; set; }

        #endregion
    }
}
