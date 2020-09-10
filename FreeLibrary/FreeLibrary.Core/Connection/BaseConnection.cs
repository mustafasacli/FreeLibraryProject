using FreeLibrary.Core.Enums;
using FreeLibrary.Core.Interfaces;
using FreeLibrary.Core.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.Core.Connection
{
    public abstract class BaseConnection : IConnection
    {
        protected DbConnection dbConn;
        protected DbTransaction dbTrans;

        protected BaseConnection(DbConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public virtual string ConnectionString
        {
            get { return this.dbConn.ConnectionString; }
            set { this.dbConn.ConnectionString = value; }
        }

        public abstract ConnectionTypes ConnectionType { get; }

        protected abstract DbDataAdapter GetDbDataAdapter();

        public virtual void Open()
        {
            try
            {
                dbConn.Open();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void BeginTransaction()
        {
            try
            {
                dbTrans = dbConn.BeginTransaction();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void CommitTransaction()
        {
            try
            {
                if (dbTrans != null)
                {
                    dbTrans.Commit();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void RollbackTransaction()
        {
            try
            {
                if (dbTrans != null && dbConn.State == ConnectionState.Open)
                {
                    dbTrans.Rollback();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public virtual void DisposeTransaction()
        {
            try
            {
                if (dbTrans != null && dbConn.State == ConnectionState.Open)
                {
                    dbTrans.Dispose();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                dbTrans = null;
            }
        }

        public virtual void Close()
        {
            try
            {
                if (dbConn.State != ConnectionState.Closed)
                { dbConn.Close(); }
            }
            catch (Exception e)
            {
            }
        }

        public virtual void Dispose()
        {
            this.Close();

            try
            { dbConn.Dispose(); }
            catch (Exception e) { }

            try
            { dbConn = null; }
            catch (Exception e) { }
        }

        public virtual DataSet GetResultSet(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sqlText))
                throw new ArgumentNullException("sqlText");

            DataSet result = new DataSet();

            try
            {
                using (DbCommand Cmd = dbConn.CreateCommand())
                {
                    Cmd.CommandText = sqlText;
                    Cmd.CommandType = cmdType;

                    if (this.dbTrans != null)
                    {
                        Cmd.Transaction = this.dbTrans;
                    }

                    if (parameters != null && parameters.Count > 0)
                    {
                        DbParameter dbParam;
                        foreach (var item in parameters)
                        {
                            dbParam = Cmd.CreateParameter();

                            dbParam.ParameterName = item.ParameterName;
                            dbParam.Value = item.Value ?? (object)DBNull.Value;
                            dbParam.DbType = item.DbType;
                            dbParam.Direction = ((int)item.Direction) == 0 ? ParameterDirection.Input : item.Direction;

                            Cmd.Parameters.Add(dbParam);
                        }
                    }

                    using (DbDataAdapter adapter = GetDbDataAdapter())
                    {
                        adapter.SelectCommand = Cmd;
                        adapter.Fill(result);
                    }

                    Cmd.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public virtual int Execute(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            int result = -1;

            try
            {
                using (DbCommand Cmd = dbConn.CreateCommand())
                {
                    Cmd.CommandText = sqlText;
                    Cmd.CommandType = cmdType;

                    if (this.dbTrans != null)
                    {
                        Cmd.Transaction = this.dbTrans;
                    }

                    if (parameters != null && parameters.Count > 0)
                    {
                        DbParameter dbParam;
                        foreach (var item in parameters)
                        {
                            dbParam = Cmd.CreateParameter();

                            dbParam.ParameterName = item.ParameterName;
                            dbParam.Value = item.Value ?? (object)DBNull.Value;
                            dbParam.DbType = item.DbType;
                            dbParam.Direction = ((int)item.Direction) == 0 ? ParameterDirection.Input : item.Direction;

                            Cmd.Parameters.Add(dbParam);
                        }
                    }

                    result = Cmd.ExecuteNonQuery();
                    Cmd.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public virtual object ExecuteScalar(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            object result = null;

            try
            {
                using (DbCommand Cmd = dbConn.CreateCommand())
                {
                    Cmd.CommandText = sqlText;
                    Cmd.CommandType = cmdType;

                    if (this.dbTrans != null)
                    {
                        Cmd.Transaction = this.dbTrans;
                    }

                    if (parameters != null && parameters.Count > 0)
                    {
                        DbParameter dbParam;
                        foreach (var item in parameters)
                        {
                            dbParam = Cmd.CreateParameter();

                            dbParam.ParameterName = item.ParameterName;
                            dbParam.Value = item.Value ?? (object)DBNull.Value;
                            dbParam.DbType = item.DbType;
                            dbParam.Direction = ((int)item.Direction) == 0 ? ParameterDirection.Input : item.Direction;

                            Cmd.Parameters.Add(dbParam);
                        }
                    }

                    result = Cmd.ExecuteScalar();
                    Cmd.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public virtual DbDataReader ExecuteReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            DbDataReader result = null;

            try
            {
                using (DbCommand Cmd = dbConn.CreateCommand())
                {
                    Cmd.CommandText = sqlText;
                    Cmd.CommandType = cmdType;

                    if (this.dbTrans != null)
                    {
                        Cmd.Transaction = this.dbTrans;
                    }

                    if (parameters != null && parameters.Count > 0)
                    {
                        DbParameter dbParam;
                        foreach (var item in parameters)
                        {
                            //dbParam = item.Parameter;
                            dbParam = Cmd.CreateParameter();

                            dbParam.ParameterName = item.ParameterName;
                            dbParam.Value = item.Value ?? (object)DBNull.Value;
                            dbParam.DbType = item.DbType;
                            dbParam.Direction = ((int)item.Direction) == 0 ? ParameterDirection.Input : item.Direction;

                            Cmd.Parameters.Add(dbParam);
                        }
                    }

                    result = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Cmd.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }



        /*
    public virtual IDbRowReader ExecuteRowReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
    {

        return new DbRowReader();
                DbDataReader result = null;

                try
                {
                    using (DbCommand Cmd = dbConn.CreateCommand())
                    {
                        Cmd.CommandText = sqlText;
                        Cmd.CommandType = cmdType;

                        if (this.dbTrans != null)
                        {
                            Cmd.Transaction = this.dbTrans;
                        }

                        if (parameters != null && parameters.Count > 0)
                        {
                            DbParameter dbParam;
                            foreach (var item in parameters)
                            {
                                //dbParam = item.Parameter;
                                dbParam = Cmd.CreateParameter();

                                dbParam.ParameterName = item.ParameterName;
                                dbParam.Value = item.Value ?? (object)DBNull.Value;
                                dbParam.DbType = item.DbType;
                                dbParam.Direction = ((int)item.Direction) == 0 ? ParameterDirection.Input : item.Direction;
                                //dbParam.Direction = item.Direction;
                                //dbParam.Precision = item.Precision;
                                //dbParam.Scale = item.Scale;
                                //dbParam.Size = item.Size;
                                //dbParam.SourceColumn = item.SourceColumn;
                                //dbParam.SourceColumnNullMapping = item.SourceColumnNullMapping;
                                //dbParam.SourceVersion = item.SourceVersion;

                                Cmd.Parameters.Add(dbParam);
                            }
                        }

                        result = Cmd.ExecuteReader();
                        Cmd.Parameters.Clear();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                return result;
    }

             */
    }
}
