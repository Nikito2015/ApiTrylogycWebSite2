namespace CommonTrylogycWebsite.DTO.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDTOAssociate
    {
        #region Properties
        /// <summary>
        /// Gets or sets the socio.
        /// </summary>
        /// <value>
        /// The socio.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the connection identifier.
        /// </summary>
        /// <value>
        /// The connection identifier.
        /// </value>
        int ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets the CGP.
        /// </summary>
        /// <value>
        /// The CGP.
        /// </value>
        string CGP { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        string Document { get; set; }

        #endregion  
    }
}
