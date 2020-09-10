using FreeLibrary.Core.Parameter;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.Core.Interfaces
{
    public interface IConnectionOperations
    {
        DataSet GetResultSet(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);

        int Execute(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);

        object ExecuteScalar(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);

        DbDataReader ExecuteReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);

        //IDbRowReader ExecuteRowReader(string sqlText, CommandType cmdType, List<FreeDbParameter> parameters = null);
    }
}
