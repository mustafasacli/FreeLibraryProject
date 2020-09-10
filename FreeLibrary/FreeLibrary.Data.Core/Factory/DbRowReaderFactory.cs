using FreeLibrary.Data.Core.Interfaces;
using System;
using System.Data.Common;

namespace FreeLibrary.Data.Core.Factory
{
    public sealed class DbRowReaderFactory
    {
        public static IDbRowReader CreateRowReader()
        {
            IDbRowReader rowReader = null;

            rowReader = new DbRowReader();

            return rowReader;
        }

        public static IDbRowReader CreateRowReader(DbDataReader reader)
        {
            IDbRowReader rowReader = null;
            try
            {
                rowReader = new DbRowReader();
                rowReader.ImportDataFromDataReader(reader);
            }
            catch (Exception e) { throw; }

            return rowReader;
        }
    }
}
