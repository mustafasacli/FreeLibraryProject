using System.Data.Common;

namespace FreeLibrary.Data.Core.Interfaces
{
    public interface IDbRowReader
    {
        IDbRow NewRow();

        IDbRowReader ImportDataFromDataReader(DbDataReader reader);

        void AddRow(IDbRow row);

        IDbRow Get(int index);

        int Count { get; }
    }
}
