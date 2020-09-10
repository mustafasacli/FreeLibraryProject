using FreeLibrary.Core.Interfaces;
using System;

namespace FreeLibrary.DataAccess.Interfaces
{
    public interface IBaseDataAccess : IConnectionOperations, IDisposable
    {
        //DataSet GetResultSet(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);
        //int Execute(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);
        //object ExecuteScalar(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);
        //DbDataReader ExecuteReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);
    }
}
