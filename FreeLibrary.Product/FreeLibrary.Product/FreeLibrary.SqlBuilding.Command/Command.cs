using FreeLibrary.SqlBuilding.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.SqlBuilding.Command
{
    /// <summary>
    /// Command object.
    /// </summary>
    public class Command : ISql
    {
        private CommandType cmdTyp =
            CommandType.StoredProcedure;

        public CommandType DbCommandType
        {
            get { return cmdTyp; }
            set { return; }
        }

        public List<DbParameter> Parameters
        { get; set; }

        public string Text
        { get; set; } = string.Empty;
    }
}
