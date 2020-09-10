using System;
using FreeLibrary.Core;
using FreeLibrary.Core.Enums;

namespace FreeLibrary.QueryBuilding
{
    /// <summary>
    /// Represents Parameter Prefix, Column-Table Prefix And Suffix and last IdentityInsert Part.
    /// </summary>
    public class QueryAdds : IQueryAdds
    {
        private string _parameterPrefix = string.Empty;
        private string _prefix = string.Empty;
        private string _suffix = string.Empty;
        private string _identityInsertAdd = string.Empty;

        /// <summary>
        /// Creates Query Object for parameter prefix, column prefix-suffix and identity insert parts.
        /// </summary>
        /// <param name="conn_type"></param>
        public QueryAdds(ConnectionTypes conn_type)
        {
            _parameterPrefix = GetParameterPrefix(conn_type);
            _prefix = GetPrefix(conn_type);
            _suffix = GetSuffix(conn_type);
            _identityInsertAdd = GetIdentityInsert(conn_type);
        }

        /// <summary>
        /// Gets Parameter Name Prefix.
        /// </summary>
        public string ParameterPrefix
        {
            get { return _parameterPrefix; }
        }

        /// <summary>
        /// Gets Table and Column Name Prefix.
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
        }

        /// <summary>
        /// Gets Table and Column Name Suffix.
        /// </summary>
        public string Suffix
        {
            get { return _suffix; }
        }

        /// <summary>
        /// Gets Identity Insert Part of Query.
        /// </summary>
        public string IdentityInsertAdd
        {
            get { return _identityInsertAdd; }
        }

        #region [ Get Parameter Prefix ]

        /// <summary>
        /// Returns Prefix for parameters according to Connection Type.
        /// </summary>
        /// <returns> Returns Prefix for parameters according to Connection Type.</returns>
        private string GetParameterPrefix(ConnectionTypes conn_type)
        {
            string _s = string.Empty;

            switch (conn_type)
            {
                case ConnectionTypes.DB2:
                case ConnectionTypes.Oledb:
                case ConnectionTypes.Sql:
                case ConnectionTypes.PgSQL:
                    _s = "@";
                    break;

                case ConnectionTypes.Oracle:
                    _s = ":";
                    break;

                case ConnectionTypes.Odbc:
                    _s = "?";
                    break;

                default:
                    break;
                    //return String.Empty;
            }

            return _s;
        }

        #endregion [ Get Parameter Prefix ]

        #region [ Get Prefix ]

        /// <summary>
        /// Returns Prefix for columns and tables according to Connection Type.
        /// </summary>
        /// <returns> Returns Prefix for columns and tables according to Connection Type.</returns>
        private string GetPrefix(ConnectionTypes conn_type)
        {
            string _prfx = string.Empty;
            switch (conn_type)
            {
                case ConnectionTypes.Oledb:
                case ConnectionTypes.Sql:
                    _prfx = "[";
                    break;

                case ConnectionTypes.PgSQL:
                    _prfx = "\"";
                    break;

                default:
                    break;
            }
            return _prfx;
        }

        #endregion [ Get Prefix ]

        #region [ Get Suffix ]

        /// <summary>
        /// Returns Suffix for columns and tables according to Connection Type.
        /// </summary>
        /// <returns>Returns Suffix for columns and tables according to Connection Type.</returns>
        private string GetSuffix(ConnectionTypes conn_type)
        {
            string _sfx = string.Empty;

            switch (conn_type)
            {
                case ConnectionTypes.Oledb:
                case ConnectionTypes.Sql:
                    _sfx = "]";
                    break;

                case ConnectionTypes.PgSQL:
                    _sfx = "\"";
                    break;

                default:
                    break;
            }

            return _sfx;
        }

        #endregion [ Get Suffix ]

        #region [ Get Identity Insert ]

        /// <summary>
        /// Returns GetIdentity query part of InsertAndGetId.
        /// </summary>
        /// <returns> Returns GetIdentity query part of InsertAndGetId. </returns>
        private string GetIdentityInsert(ConnectionTypes conn_type)
        {
            string _s = string.Empty;
            switch (conn_type)
            {
                case ConnectionTypes.Oledb:
                case ConnectionTypes.Sql:
                    //_s = "SELECT SCOPE_IDENTITY();";//String.Format("SELECT IDENT_CURRENT('{0}');", _baseBO.GetTable());
                    _s = string.Format("SELECT {0}#ID_COL# = SCOPE_IDENTITY();", _parameterPrefix);
                    break;

                case ConnectionTypes.Oracle:
                    _s = string.Format(" Returning {0}#ID_COL#{1} As IdCol", _prefix, _suffix);
                    break;

                case ConnectionTypes.SQLite:
                    _s = "select last_insert_rowid();";
                    break;

                case ConnectionTypes.MySql:
                    _s = "SELECT LAST_INSERT_ID();";
                    break;

                case ConnectionTypes.PgSQL:
                    _s = "SELECT LASTVAL();";
                    break;

                default:
                    break;
            }
            return _s;
        }

        #endregion [ Get Identity Insert ]
    }
}