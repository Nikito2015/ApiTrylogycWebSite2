using CommonTrylogycWebsite.DTO.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CommonTrylogycWebsite.Models
{

    /// <summary>
    /// WcfUser class
    /// </summary>
    public class WcfUser
    {
        #region Constructors

        public WcfUser()
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

        public List<WcfAssociateConnection> AssociatesConnections { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email invoice].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [email invoice]; otherwise, <c>false</c>.
        /// </value>

        public int TotalConnections { get; set; }

        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>

        public List<WcfAssociate> Associates { get; set; }

        /// <summary>
        /// Gets or sets the balances.
        /// </summary>
        /// <value>
        /// The balances.
        /// </value>

        public List<WcfBalance> Balances { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <typeparam name="DTOUser">The type of to user.</typeparam>
        /// <param name="dtoUser">The dto user.</param>
        /// <returns></returns>
        public static WcfUser CreateFromDTO(IDTOUser dtoUser)
        {
            if (dtoUser == null)
                throw new System.Exception("Source DTO object can not be null to create user.");
            return new WcfUser()
            {
                Id = dtoUser.Id,
                UserName = dtoUser.UserName,
                Password = dtoUser.Password,
                Email = dtoUser.Email,
                Picture = dtoUser.Email,
                Route = dtoUser.Route,
                EmailInvoice = dtoUser.EmailInvoice,
                AssociatesConnections = dtoUser.AssociatesConnections?
                                        .Select(a => new WcfAssociateConnection()
                                        {
                                            AssociateId = a.AssociateId,
                                            ConnectionId = a.ConnectionId
                                        })?.ToList(),
                Balances = dtoUser.Balances?
                           .Select(a => new WcfBalance()
                           {
                               AssociateId = a.AssociateId,
                               ConnectionId = a.ConnectionId,
                               InvoiceAmmount = a.InvoiceAmmount,
                               InvoiceDate = a.InvoiceDate,
                               InvoiceExpirationDate = a.InvoiceExpirationDate,
                               InvoiceGroup = a.InvoiceGroup,
                               InvoiceLetter = a.InvoiceLetter,
                               InvoiceNumber = a.InvoiceNumber,
                               InvoicePoint = a.InvoicePoint,
                               InvoiceTrackingNumber = a.InvoiceTrackingNumber,
                               Paid = a.Paid,
                               Period = a.Period
                           })?.ToList(),
                Associates = dtoUser.Associates?
                             .Select(a => new WcfAssociate()
                             {
                                 Id = a.Id,
                                 Address = a.Address,
                                 CGP = a.CGP,
                                 City = a.City,
                                 ConnectionId = a.ConnectionId,
                                 Document = a.Document,
                                 Name = a.Name
                             })?.ToList()


            };
        }

        private void aees()
        {
            var a = Associates.Select(elem => new {
                tipo = elem.Address,
                casa = elem.City
            });
        }
        #endregion
    }


}

