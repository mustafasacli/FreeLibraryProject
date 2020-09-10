using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeLibrary.SqlBuilding.Core.Interfaces;
using System.Data.Common;

namespace FreeLibrary.SqlBuilding.Command
{
    public class CommandBuilder : ISqlBuilder
    {
        Command cmd = new Command();

        public ISql Sql { get { return cmd; } }

        public void BuildSql()
        {
            return;
        }

        public DbParameter CreateParameter()
        {
            throw new NotImplementedException();
        }
    }
}

