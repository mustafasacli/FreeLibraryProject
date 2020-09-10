using System;
using FreeLibrary.Core;
using FreeLibrary.Data.DB2;
using FreeLibrary.Data.Odbc;
using FreeLibrary.Data.OleDb;
using FreeLibrary.Data.Oracle;
using FreeLibrary.Data.PostgreSQL;
using FreeLibrary.Data.Sql;
using FreeLibrary.Core.Enums;
using FreeLibrary.Core.Interfaces;

namespace FreeLibrary.CodeGeneration.Source.Factory
{
    public sealed class ConnectionFactory
    {

        #region [ CreateConnection Method ]

        /// <summary>
        /// Creates IConnection object with given parameters.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <returns>Returns IConnection object.</returns>
        public static IConnection CreateConnection(ConnectionTypes connType)
        {
            IConnection conn = null;
            try
            {
                switch (connType)
                {
                    case ConnectionTypes.DB2:
                        conn = new ConnectionDB2();
                        break;

                    case ConnectionTypes.Oracle:
                        conn = new ConnectionOracle();
                        break;

                    case ConnectionTypes.Oledb:
                        conn = new ConnectionOleDb();
                        break;

                    case ConnectionTypes.Odbc:
                        conn = new ConnectionOdbc();
                        break;

                    case ConnectionTypes.PgSQL:
                        conn = new ConnectionPgSql();
                        break;

                    case ConnectionTypes.Sql:
                        conn = new ConnectionSql();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return conn;
        }

        #endregion


        #region [ CreateConnection Method ]
        /// <summary>
        /// Creates IConnection object with given parameters.
        /// </summary>
        /// <param name="connType">Connection Type</param>
        /// <param name="connectionString">Connection String</param>
        /// <returns>Returns IConnection object.</returns>
        public static IConnection CreateConnection(ConnectionTypes connType, string connectionString)
        {
            IConnection conn = null;
            try
            {
                switch (connType)
                {
                    case ConnectionTypes.DB2:
                        conn = new ConnectionDB2(connectionString);
                        break;

                    case ConnectionTypes.Oracle:
                        conn = new ConnectionOracle(connectionString);
                        break;

                    case ConnectionTypes.Oledb:
                        conn = new ConnectionOleDb(connectionString);
                        break;

                    case ConnectionTypes.Odbc:
                        conn = new ConnectionOdbc(connectionString);
                        break;

                    case ConnectionTypes.PgSQL:
                        conn = new ConnectionPgSql(connectionString);
                        break;

                    case ConnectionTypes.Sql:
                        conn = new ConnectionSql(connectionString);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return conn;
        }

        #endregion

    }
}
