using BLLTrylogycWebsite.Enums;
using BLLTrylogycWebsite.Interfaces;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTrylogycWebsite
{

    /// <summary>
    /// Base Class for BLL
    /// </summary>
    public class BLLBase : IBLLBase
    {

        #region Private Members

        protected ILog _log;
        protected string _connectionString;
        protected IConfiguration _configuration;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BLLBase"/> class.
        /// </summary>
        public BLLBase(ILog log, string connectionString, IConfiguration configuration)
        {
            _log = log;
            _connectionString = connectionString;
            _configuration = configuration;
            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("Connection string required to initialize BLLBase");
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

    }
}
