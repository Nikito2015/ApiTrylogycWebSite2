using BLLTrylogycWebsite.Interfaces;
using BLLTrylogycWebsite.Responses.Interfaces;
using CommonTrylogycWebsite.DTO;
using CommonTrylogycWebsite.DTO.Interfaces;
using System.Collections.Generic;

namespace BLLTrylogycWebsite.Models.Interfaces
{


    /// <summary>
    /// Interface for a BLLUser
    /// </summary>
    public interface IBLLUser: IBLLBase
    {
        #region Methods
        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IBLLResponseBase<IDTOUser> Login(string userEmail, string password, bool applyConnectionFiltering);

        /// <summary>
        /// Gets the invoice PDF.
        /// </summary>
        /// <param name="associateId">The associate identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <param name="retrieveFromFtp">if set to <c>true</c> [retrieve from FTP].</param>
        /// <returns></returns>
        IBLLResponseBase<string> GetInvoicePdf(int associateId, int connectionId, string invoiceNumber, bool retrieveFromFtp);

        /// <summary>
        /// Registers the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="emailConfirm">The email confirm.</param>
        /// <param name="code">The code.</param>
        /// <param name="CGP">The CGP.</param>
        /// <returns></returns>
        IBLLResponseBase<bool> Register(string email, string emailConfirm, string code, string CGP, bool emailInvoices);

        /// <summary>
        /// Adds the relation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="associateCode">The associate code.</param>
        /// <param name="cgp">The CGP.</param>
        /// <returns></returns>
        IBLLResponseBase<bool> AddRelation(int userId, string associateCode, string cgp);

        /// <summary>
        /// Gets the user associates.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IBLLResponseBase<IEnumerable<IDTOAssociate>> GetUserAssociates(int userId);

        /// <summary>
        /// Gets the user balances.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IBLLResponseBase<IEnumerable<IDTOBalance>> GetUserBalances(int userId);

        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="sendInvoiceMail">if set to <c>true</c> [send invoice mail].</param>
        /// <returns></returns>
        IBLLResponseBase<bool> UpdateUserData(string userName, string oldPassword, string newPassword, bool sendInvoiceMail);

        /// <summary>
        /// Retrieves the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="cgp">The CGP.</param>
        /// <returns></returns>
        IBLLResponseBase<bool> RetrievePassword(string email, string cgp);
        #endregion
    }
}