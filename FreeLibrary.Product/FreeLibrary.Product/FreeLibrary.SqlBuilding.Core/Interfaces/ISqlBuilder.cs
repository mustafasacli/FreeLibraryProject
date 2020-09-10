using System.Data.Common;

namespace FreeLibrary.SqlBuilding.Core.Interfaces
{
    public interface ISqlBuilder
    {
        void BuildSql();

        ISql Sql { get; }

        DbParameter CreateParameter();
    }
}
