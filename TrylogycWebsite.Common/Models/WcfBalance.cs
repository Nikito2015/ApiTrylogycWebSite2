using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CommonTrylogycWebsite.Models
{

    /// <summary>
    /// WcfBalance class
    /// </summary>
    public class WcfBalance
    {

        #region Properties

        public int AssociateId { get; set; }


        public int ConnectionId { get; set; }


        public string Period { get; set; }


        public string InvoiceGroup { get; set; }


        public string InvoiceLetter { get; set; }


        public string InvoicePoint { get; set; }


        public string InvoiceNumber { get; set; }


        public string InvoiceDate { get; set; }


        public string InvoiceExpirationDate { get; set; }


        public string InvoiceAmmount { get; set; }


        public string InvoiceTrackingNumber { get; set; }


        public bool Paid { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the balances collection from dto.
        /// </summary>
        /// <param name="balances">The balances.</param>
        /// <returns></returns>
        public static List<WcfBalance> CreateBalancesCollectionFromDTO(IEnumerable<IDTOBalance> balances)
        {
            return balances?
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
                           })?.ToList();
        }
        #endregion
    }
}