using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDTOBalance
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

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        string Period { get; set; }

        /// <summary>
        /// Gets or sets the invoice group.
        /// </summary>
        /// <value>
        /// The invoice group.
        /// </value>
        string InvoiceGroup { get; set; }

        /// <summary>
        /// Gets or sets the invoice letter.
        /// </summary>
        /// <value>
        /// The invoice letter.
        /// </value>
        string InvoiceLetter { get; set; }

        /// <summary>
        /// Gets or sets the invoice point.
        /// </summary>
        /// <value>
        /// The invoice point.
        /// </value>
        string InvoicePoint { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        /// <value>
        /// The invoice date.
        /// </value>
        string InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice expiration date.
        /// </summary>
        /// <value>
        /// The invoice expiration date.
        /// </value>
        string InvoiceExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice ammount.
        /// </summary>
        /// <value>
        /// The invoice ammount.
        /// </value>
        string InvoiceAmmount { get; set; }

        /// <summary>
        /// Gets or sets the invoice tracking number.
        /// </summary>
        /// <value>
        /// The invoice tracking number.
        /// </value>
        string InvoiceTrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IDTOBalance"/> is paid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paid; otherwise, <c>false</c>.
        /// </value>
        bool Paid { get; set; }
        #endregion
    }
}
