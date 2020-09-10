using FreeLibrary.Data.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace FreeLibrary.Data.Core.Factory
{
    internal class DbRowReader : IDbRowReader
    {
        private Queue<IDbRow> qRows = null;

        public DbRowReader()
        {
            qRows = new Queue<IDbRow>();
        }

        public IDbRow NewRow()
        {
            return new DbRow();
        }

        public IDbRowReader ImportDataFromDataReader(DbDataReader reader)
        {
            DbRowReader dbReader = null;

            try
            {
                if (reader == null)
                    throw new ArgumentNullException(nameof(reader));

                if (reader.IsClosed)
                    throw new Exception("DbDataReader is already closed.");

                dbReader = new DbRowReader();
                if (reader.HasRows)
                {
                    IDbRow row;
                    //IDbCell cell;
                    int count;

                    while (reader.Read())
                    {
                        row = dbReader.NewRow();
                        count = reader.FieldCount;
                        for (int counter = 0; counter < count; counter++)
                        {
                            //cell = row.CreateCell(reader.GetName(counter), reader.GetFieldType(counter), reader[counter]);
                            //row.AddCell(cell);
                            row.AddCell(reader.GetName(counter), reader.GetFieldType(counter), reader[counter]);
                        }
                    }
                }
            }
            catch (Exception e) { throw; }

            return dbReader;
        }

        public void AddRow(IDbRow row)
        {
            qRows.Enqueue(row);
        }

        public IDbRow Get(int index)
        {
            IDbRow row = qRows.ElementAt(index);
            return row;
        }

        public int Count { get { return qRows.Count; } }
    }
}
