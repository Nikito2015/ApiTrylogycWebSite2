using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace CommonTrylogycWebsite.DTO.Interfaces
{

    /// <summary>
    /// Interface for a user.
    /// </summary>
    public interface IDTOUser
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enviar factura email].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enviar factura email]; otherwise, <c>false</c>.
        /// </value>
        bool EmailInvoice { get; set; }

        /// <summary>
        /// Gets or sets the foto.
        /// </summary>
        /// <value>
        /// The foto.
        /// </value>
        string Picture { get; set; }

        /// <summary>
        /// Gets or sets the identifier usuario.
        /// </summary>
        /// <value>
        /// The identifier usuario.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the ruta.
        /// </summary>
        /// <value>
        /// The ruta.
        /// </value>
        string Route { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        IEnumerable<IDTOAssociateConnection> AssociatesConnections { get; set; }

        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>
        IEnumerable<IDTOAssociate> Associates { get; set; }

        /// <summary>
        /// Gets or sets the balances.
        /// </summary>
        /// <value>
        /// The balances.
        /// </value>
        IEnumerable<IDTOBalance> Balances { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Adds the balances.
        /// </summary>
        void AddBalancesFromDataTable(DataTable balances);

        /// <summary>
        /// Adds the associates.
        /// </summary>
        void AddAssociatesFromDataTable(DataTable associates);
        #endregion

    }
}
