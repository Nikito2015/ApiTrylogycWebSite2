using CommonTrylogycWebsite.DTO.Extensions;
using CommonTrylogycWebsite.DTO.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
 
using System.Linq;

namespace CommonTrylogycWebsite.DTO
{

    /// <summary>
    /// User class.
    /// </summary>
    public class DTOUser : IDTOUser
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DTOUser"/> class.
        /// </summary>
        public DTOUser()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the identifier usuario.
        /// </summary>
        /// <value>
        /// The identifier usuario.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the foto.
        /// </summary>
        /// <value>
        /// The foto.
        /// </value>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the ruta.
        /// </summary>
        /// <value>
        /// The ruta.
        /// </value>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enviar factura email].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enviar factura email]; otherwise, <c>false</c>.
        /// </value>
        public bool EmailInvoice { get; set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        [JsonConverter(typeof(InterfaceToConcreteConverter<IDTOAssociateConnection, DTOAssociateConnection>))]
        public IEnumerable<IDTOAssociateConnection> AssociatesConnections { get; set; } = new List<DTOAssociateConnection>();

        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>
        [JsonConverter(typeof(InterfaceToConcreteConverter<IDTOAssociate, DTOAssociate>))]
        public IEnumerable<IDTOAssociate> Associates { get; set; }

        /// <summary>
        /// Gets or sets the balances.
        /// </summary>
        /// <value>
        /// The balances.
        /// </value>
        [JsonConverter(typeof(InterfaceToConcreteConverter<IDTOBalance, DTOBalance>))]
        public IEnumerable<IDTOBalance> Balances { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Values the tuple.
        /// </summary>
        /// <typeparam name="DataRow">The type of the ata row.</typeparam>
        /// <returns></returns>
        public static DTOUser CreateFromDataRow(DataRow userRow)
        {
            if (userRow == null)
                throw new ArgumentNullException("Row must not be null to create a DTOUser");

            return new DTOUser()
            {
                Id = Convert.ToInt32(userRow["IDUsuario"]),
                Email = userRow["UserEmail"]?.ToString(),
                EmailInvoice = (userRow["EnviaFacturaMail"] != DBNull.Value) ? Convert.ToBoolean(userRow["EnviaFacturaMail"]) : false,
                Picture = userRow["Foto"]?.ToString(),
                Password = userRow["UserPass"]?.ToString(),
                Route = userRow["Ruta"]?.ToString(),
                UserName = userRow["UserName"]?.ToString()
            };
        }

        /// <summary>
        /// Adds the balances.
        /// </summary>
        public void AddBalancesFromDataTable(DataTable balances)
        {
           Balances = Balances.PopulateBalancesCollectionFromDataTable(balances);
        }

        /// <summary>
        /// Adds the associates.
        /// </summary>
        public void AddAssociatesFromDataTable(DataTable associates)
        {
            Associates = Associates.PopulateAssociatesCollectionFromDataTable(associates);
        }

        #endregion
    }

}

