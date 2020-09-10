using System;

namespace FreeLibrary.Data.Core.Interfaces
{
    public interface IDbCell
    {
        string CellName { get; }

        Type CellDataType { get; }

        object CellValue { get; set; }
    }
}
