using FreeLibrary.Data.Core.Interfaces;
using System;

namespace FreeLibrary.Data.Core.Factory
{
    internal class DbCell : IDbCell
    {
        public DbCell(string cellName, Type cellDataType) : this(cellName, cellDataType, null)
        {
        }

        public DbCell(string cellName, Type cellDataType, object cellValue)
        {
            this.CellName = cellName;
            this.CellDataType = cellDataType;
            this.CellValue = CellValue;
        }

        public string CellName { get; private set; }

        public Type CellDataType { get; private set; }

        public object CellValue { get; set; }
    }
}
