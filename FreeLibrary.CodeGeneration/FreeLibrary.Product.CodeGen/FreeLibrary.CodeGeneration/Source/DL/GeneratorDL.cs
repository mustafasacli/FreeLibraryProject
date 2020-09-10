using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using FreeLibrary.CodeGeneration.Source.Factory;
using FreeLibrary.CodeGeneration.Source.QO;
using FreeLibrary.Core;
using FreeLibrary.Core.Interfaces;
using FreeLibrary.Core.Enums;
using System.Reflection;

namespace FreeLibrary.CodeGeneration.Source.DL
{
    internal class GeneratorDL
    {
        #region [ GetSqlServerTables method ]

        public DataTable GetSqlServerTables(string connectionString)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IConnection freeConn = ConnectionFactory.CreateConnection(ConnectionTypes.Sql, connectionString))
                {
                    dt = freeConn.GetResultSet(Crud.GetTablesAndColumns(ConnectionTypes.Sql), CommandType.Text).Tables[0];
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dt;
        }

        #endregion [ GetSqlServerTables method ]

        #region [ GetPostgreSQL method ]

        public DataTable GetPostgreSQL(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (IConnection freeConn = ConnectionFactory.CreateConnection(ConnectionTypes.PgSQL, connectionString))
                {
                    dt = freeConn.GetResultSet(Crud.GetTablesAndColumns(ConnectionTypes.PgSQL), CommandType.Text).Tables[0];
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return dt;
        }

        #endregion [ GetPostgreSQL method ]

        #region [ GetOracleTables method ]

        public DataTable GetOracleTables(string connectionString)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IConnection freeConn = ConnectionFactory.CreateConnection(ConnectionTypes.Oracle, connectionString))
                {
                    dt = freeConn.GetResultSet(Crud.GetTablesAndColumns(ConnectionTypes.Oracle), CommandType.Text).Tables[0];
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dt;
        }

        #endregion [ GetOracleTables method ]

        #region [ GetOledbSet method ]

        public DataSet GetOledbSet(string connectionString)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OleDbConnection oledbConn = new OleDbConnection(connectionString))
                {
                    oledbConn.Open();
                    DataTable dt2 = new DataTable();
                    dt2 = oledbConn.GetType().GetMethod("GetSchema", new Type[] { typeof(string), typeof(string[]) }).Invoke(oledbConn, new object[] { "TABLES", new string[] { null, null, null, "Table" } }) as DataTable;
                    DataTable dt = oledbConn.GetSchema("TABLES", new string[] { null, null, null, "Table" });
                    DataView dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc";
                    dt = dV.ToTable();

                    ds.Tables.Add(dt);
                    //System.Data.DataTable dt = null;

                    // Get the data table containing the schema
                    //dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    /*
                     * DataTable dbSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, "tableName", null });
        foreach (DataRow row in dbSchema.Rows)
        {
            string MyName = row["COLUMN_NAME"].ToString();
            string myDT = row["DATA_TYPE"].ToString();
        }
                     */
                    dt = new DataTable();
                    dt = oledbConn.GetSchema("COLUMNS", new string[] { null, null, null, null });
                    dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc, ORDINAL_POSITION Asc";
                    dt = dV.ToTable();
                    ds.Tables.Add(dt);

                    oledbConn.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return ds;
        }

        #endregion [ GetOledbSet method ]

        #region [ GetOdbcTable method ]

        public DataSet GetOdbcTable(string connectionString)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OdbcConnection odbcConn = new OdbcConnection(connectionString))
                {
                    odbcConn.Open();
                    DataTable dt = odbcConn.GetSchema("TABLES");
                    DataView dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc";
                    dt = dV.ToTable();

                    ds.Tables.Add(dt);

                    dt = new DataTable();
                    dt = odbcConn.GetSchema("COLUMNS");
                    dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc, ORDINAL_POSITION Asc";
                    dt = dV.ToTable();
                    ds.Tables.Add(dt);

                    odbcConn.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return ds;
        }

        #endregion [ GetOdbcTable method ]

        #region [ GetDB2Tables method ]

        public DataTable GetDB2Tables(string connectionString)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IConnection freeConn = ConnectionFactory.CreateConnection(ConnectionTypes.DB2, connectionString))
                {
                    dt = freeConn.GetResultSet(Crud.GetTablesAndColumns(ConnectionTypes.DB2), CommandType.Text).Tables[0];
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dt;
        }

        #endregion [ GetDB2Tables method ]
    }
}