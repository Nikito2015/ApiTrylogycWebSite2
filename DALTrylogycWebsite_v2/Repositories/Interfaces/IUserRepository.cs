using CommonTrylogycWebsite.DTO.Interfaces;
using DALTrylogycWebsite.DALResponses.Interfaces;

namespace DALTrylogycWebsite.Repositories.Interfaces
{

    /// <summary>
    /// Interface for a UserRepository
    /// </summary>
    public interface IUserRepository
    {

        #region Methods

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        IBaseDALResponse GetUserByEmail(string userEmail);

        /// <summary>
        /// Gets the connections by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IBaseDALResponse GetConnectionsByUserId(int userId);

        /// <summary>
        /// Registers the user with the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="Code">The code.</param>
        /// <param name="CGP">The CGP.</param>
        /// <param name="emailInvoices">if set to <c>true</c> [email invoices].</param>
        /// <returns></returns>
        IBaseDALResponse Register(string email, int associateId, int conectionId, bool emailInvoices);

        /// <summary>
        /// Gets the user count by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        IBaseDALResponse GetUserCountByEmail(string email);

        /// <summary>
        /// Puts the relacion.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="associateId">The associate identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        IBaseDALResponse PutRelacion(int userId, int associateId, int connectionId);

        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="sendInvoiceEmail">if set to <c>true</c> [send invoice email].</param>
        /// <returns></returns>
        IBaseDALResponse UpdateUserData(int userId, string newPassword, bool sendInvoiceEmail);

        /// <summary>
        /// Gets the user by email and associate identifier.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="associateId">The associate identifier.</param>
        /// <returns></returns>
        IBaseDALResponse GetUserByEmailAndAssociateId(string email, int associateId);
        #endregion
    }
}
