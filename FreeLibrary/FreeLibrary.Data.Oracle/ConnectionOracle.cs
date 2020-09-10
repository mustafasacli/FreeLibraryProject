using FreeLibrary.Core.Connection;
using FreeLibrary.Core.Enums;
using FreeLibrary.Core.Parameter;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.Data.Oracle
{
    public class ConnectionOracle : BaseConnection
    {
        public ConnectionOracle()
            : this(string.Empty)
        {
        }

        public ConnectionOracle(string connectionString) : base(new OracleConnection())
        {
            this.ConnectionString = connectionString ?? string.Empty;
        }

        public override string ConnectionString
        {
            get { return base.ConnectionString; }
            set { base.ConnectionString = value; }
        }

        public override ConnectionTypes ConnectionType
        {
            get { return ConnectionTypes.DB2; }
        }

        public override void Open()
        {
            base.Open();
        }

        public override void BeginTransaction()
        {
            base.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();
        }

        public override void RollbackTransaction()
        {
            base.RollbackTransaction();
        }

        public override void DisposeTransaction()
        {
            base.DisposeTransaction();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override DbDataAdapter GetDbDataAdapter()
        {
            return new OracleDataAdapter();
        }

        public override DataSet GetResultSet(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            return base.GetResultSet(sqlText, cmdType, parameters);
        }

        public override int Execute(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            return base.Execute(sqlText, cmdType, parameters);
        }

        public override object ExecuteScalar(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            return base.ExecuteScalar(sqlText, cmdType, parameters);
        }

        public override DbDataReader ExecuteReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null)
        {
            return base.ExecuteReader(sqlText, cmdType, parameters);
        }
    }
}
