using System;
using System.Collections;

namespace FreeLibrary.Data.Core.Interfaces
{
    public interface IDbRow : IEnumerable
    {
        void AddCell(IDbCell cell);

        void AddCell(string cellName, Type cellDataType, object cellValue);

        IDbCell CreateCell(string cellName, Type cellDataType);

        IDbCell CreateCell(string cellName, Type cellDataType, object cellValue);
    }
}
