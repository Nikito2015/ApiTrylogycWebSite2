using DALTrylogycWebsite.DALResponses;
using DALTrylogycWebsite.DALResponses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using log4net;
using System;
using System.Data.SqlClient;


namespace DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        #region Private Members
        protected ILog _log;
        private string _connString;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public BaseRepository(ILog log, string connString)
        {
            _log = log;
            _log.Info("BaseRepository() Comienzo...");
            _log.Info($"Setting connection string to {connString}");
            _connString = connString;
            Connection = new SqlConnection(_connString);
            Command = new SqlCommand("", Connection);
        }

        #endregion

        #region Private Members
        private bool _disposed = false;
        #endregion

        #region Public Properties
        public SqlConnection Connection { get;private set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader DataReader { get; set; }
        #endregion

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);          
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Connection.Close();
                    Connection.Dispose();                   
                    Command.Dispose();                   
                    DataReader.Close();
                    Connection = null;
                    Command = null;
                    DataReader = null;
                }
                _disposed = true;
            }
        }

        ~BaseRepository()
        {
            dispose(false);
        }

        protected void VerifyConnectionAndCommand()
        {
            if (Connection == null)
                Connection = new SqlConnection(_connString);

            if (Command == null)
                Command = new SqlCommand("", Connection);
        }
    }
}
