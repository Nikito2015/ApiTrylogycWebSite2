using CommonTrylogycWebsite.DTO.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CommonTrylogycWebsite.Models
{

    /// <summary>
    /// Class that represents a wcf Associate
    /// </summary>
    public class WcfAssociate
    {
        #region Public Properties


        public int Id { get; set; }


        public int ConnectionId { get; set; }


        public string CGP { get; set; }


        public string Name { get; set; }


        public string Address { get; set; }


        public string City { get; set; }


        public string Document { get; set; }

        #endregion

        /// <summary>
        /// Creates the associates collection from dto.
        /// </summary>
        /// <param name="associates">The associates.</param>
        /// <returns></returns>
        public static List<WcfAssociate> CreateAssociatesCollectionFromDTO(IEnumerable<IDTOAssociate> associates)
        {
            return associates?
                             .Select(a => new WcfAssociate()
                             {
                                 Id = a.Id,
                                 Address = a.Address,
                                 CGP = a.CGP,
                                 City = a.City,
                                 ConnectionId = a.ConnectionId,
                                 Document = a.Document,
                                 Name = a.Name
                             })?.ToList();
        }
    }
}