using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.SqlBuilding.Core.Interfaces
{
    public interface ISql
    {
        /// <summary>
        /// Gets parameters list.
        /// </summary>
        List<DbParameter> Parameters { get; set; }

        /// <summary>
        /// Gets Sql Command.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets Sql Command Type.
        /// </summary>
        CommandType DbCommandType { get; set; }
    }
}
