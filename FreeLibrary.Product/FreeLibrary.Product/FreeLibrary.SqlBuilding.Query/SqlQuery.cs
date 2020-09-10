using FreeLibrary.SqlBuilding.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLibrary.SqlBuilding.Query
{
    public class SqlQuery : ISql
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
