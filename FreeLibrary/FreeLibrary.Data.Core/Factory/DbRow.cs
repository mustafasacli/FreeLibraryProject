using FreeLibrary.Data.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FreeLibrary.Data.Core.Factory
{
    internal class DbRow : IDbRow
    {
        private Queue<IDbCell> qCells = null;

        public DbRow()
        {
            qCells = new Queue<IDbCell>();
        }

        public IDbCell this[int index]
        {
            get { return qCells.ElementAt(index); }
        }

        public int Count { get { return qCells.Count; } }

        public void AddCell(IDbCell cell)
        {
            qCells.Enqueue(cell);
        }

        public void AddCell(string cellName, Type cellDataType, object cellValue)
        {
            qCells.Enqueue(new DbCell(cellName, cellDataType, cellValue));
        }

        public IDbCell CreateCell(string cellName, Type cellDataType)
        {
            return new DbCell(cellName, cellDataType);
        }

        public IDbCell CreateCell(string cellName, Type cellDataType, object cellValue)
        {
            return new DbCell(cellName, cellDataType, cellValue);
        }

        public IEnumerator GetEnumerator()
        {
            return qCells.GetEnumerator();
        }
    }
}
