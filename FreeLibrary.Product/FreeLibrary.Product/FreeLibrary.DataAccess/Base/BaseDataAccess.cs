using FreeLibrary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FreeLibrary.Core.Connection;
using FreeLibrary.Core.Enums;
using FreeLibrary.Core.Parameter;

namespace FreeLibrary.DataAccess.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDataAccess : IBaseDataAccess
    {

        #region [ protected fields ]

        protected BaseConnection pConn;

        #endregion


        #region [ BaseDataAccess ctors ]

        /// <summary>
        /// protected ctor of BaseDL.
        /// </summary>
        protected BaseDataAccess(BaseConnection Conn)
        {
            pConn = Conn;
        }

        #endregion


        #region [ Dispose method ]

        /// <summary>
        /// Disposes DbManager object with sub objects.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (pConn != null)
                {
                    pConn.Dispose();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                pConn = null;
            }
        }

        public DataSet GetResultSet(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            DataSet result = new DataSet();

            try
            {
                result = this.pConn.GetResultSet(sqlText, cmdType, parameters);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                this.pConn.Close();
            }

            return result;
        }

        public int Execute(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            int result = -100;

            try
            {
                this.pConn.Open();
                this.pConn.BeginTransaction();

                result = this.pConn.Execute(sqlText, cmdType, parameters);

                this.pConn.CommitTransaction();
            }
            catch (Exception e)
            {
                this.pConn.RollbackTransaction();
                throw;
            }
            finally
            {
                this.pConn.DisposeTransaction();
                this.pConn.Close();
            }

            return result;
        }

        public object ExecuteScalar(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            object result = null;

            try
            {
                this.pConn.Open();
                this.pConn.BeginTransaction();

                result = this.pConn.ExecuteScalar(sqlText, cmdType, parameters);

                this.pConn.CommitTransaction();
                this.pConn.DisposeTransaction();
            }
            catch (Exception e)
            {
                this.pConn.RollbackTransaction();
                this.pConn.DisposeTransaction();
                throw;
            }
            finally
            {
                this.pConn.Close();
            }

            return result;
        }

        public DbDataReader ExecuteReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            DbDataReader result = null;

            try
            {
                this.pConn.Open();
                //this.pConn.BeginTransaction();

                result = this.pConn.ExecuteReader(sqlText, cmdType, parameters);
                return result;
                //this.pConn.CommitTransaction();
                //this.pConn.DisposeTransaction();
            }
            catch (Exception e)
            {
                //this.pConn.RollbackTransaction();
                //this.pConn.DisposeTransaction();
                throw;
            }
            finally
            {
                this.pConn.Close();
            }

            return result;
        }

        #endregion


        #region [ BaseDL Destructor ]

        ~BaseDataAccess()
        {
            Dispose();
        }

        #endregion


        #region [ ConnectionType Property ]

        /// <summary>
        /// Gets ConnectionType of Base DataLayer.
        /// </summary>
        public ConnectionTypes ConnectionType
        {
            get { return this.pConn.ConnectionType; }
        }

        #endregion

    }
}