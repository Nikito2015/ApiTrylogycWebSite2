using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommonTrylogycWebsite.DTO.Extensions
{

    /// <summary>
    /// Extensions for a DTOAssociate
    /// </summary>
    public static class DTOAssociateExtensions
    {
        #region Methods
        /// <summary>
        /// Creates the associates collection from data table.
        /// </summary>
        /// <param name="associates">The associates.</param>
        /// <param name="associatesRecords">The associates records.</param>
        /// <returns></returns>
        public static IEnumerable<IDTOAssociate> PopulateAssociatesCollectionFromDataTable(this IEnumerable<IDTOAssociate> associates, DataTable associatesRecords)
        {
            associates = from row in associatesRecords?.AsEnumerable()
            select new DTOAssociate
            {
                Id = Convert.ToInt32(row.Field<string>("Socio")),
                ConnectionId = Convert.ToInt32(row.Field<string>("Conexion")),
                Name = row.Field<string>("Nombre"),
                Address = row.Field<string>("Direcion"),
                City = row.Field<string>("Localidad"),
                CGP = row.Field<string>("CGP"),
                Document = row.Field<string>("Documento")
            };
            return associates;
        } 
        #endregion
    }
}
