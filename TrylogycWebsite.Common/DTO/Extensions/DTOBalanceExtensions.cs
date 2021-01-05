using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO.Extensions
{

    /// <summary>
    /// Extensions for a DTOBalance
    /// </summary>
    public static class DTOBalanceExtensions
    {
        #region Methods

        /// <summary>
        /// Populates the balances collection from data table.
        /// </summary>
        /// <param name="balances">The balances.</param>
        /// <param name="balancesRecords">The balances records.</param>
        public static IEnumerable<IDTOBalance> PopulateBalancesCollectionFromDataTable(this IEnumerable<IDTOBalance> balances, DataTable balancesRecords)
        {
            balances = from row in balancesRecords?.AsEnumerable()
                       select new DTOBalance
                       {
                           AssociateId = Convert.ToInt32(row.Field<string>("Socio")),
                           ConnectionId = Convert.ToInt32(row.Field<string>("Conexion")),
                           Period = row.Field<string>("Periodo"),
                           InvoiceGroup = row.Field<string>("Grupo_Fact"),
                           InvoiceLetter = row.Field<string>("Letra"),
                           InvoicePoint = row.Field<string>("Pto_Venta"),
                           InvoiceNumber = row.Field<string>("Numero"),
                           InvoiceTrackingNumber = row.Field<string>("Factura"),
                           InvoiceDate = row.Field<string>("Fecha_Emision"),
                           InvoiceExpirationDate = row.Field<string>("Fecha_Vto"),
                           InvoiceAmmount = row.Field<string>("Importe"),
                           Paid = Convert.ToBoolean(Convert.ToInt32(row.Field<string>("Pagada")))
                       };
            return balances;
        }

        #endregion
    }
}
