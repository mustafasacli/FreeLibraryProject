using System;
using System.Data;
using System.Data.Common;

namespace FreeLibrary.Core.Parameter
{
    public sealed class FreeDbParameter : DbParameter, ICloneable
    {
        private DbType pDbType;
        private ParameterDirection pDirection;
        private bool pIsNullable;
        private string pName;
        private int pSize;
        private string pSourceColumn;
        private bool pSourceColumnNullMapping;
        private object pValue;

        public FreeDbParameter()
        {
        }

        public FreeDbParameter(DbParameter dbParam)
        {
            this.DbType = dbParam.DbType;
            this.Direction = dbParam.Direction;
            this.IsNullable = dbParam.IsNullable;

            this.ParameterName = dbParam.ParameterName;
            this.Size = dbParam.Size;
            this.SourceColumn = dbParam.SourceColumn;

            this.SourceColumnNullMapping = dbParam.SourceColumnNullMapping;
            this.Value = dbParam.Value;
        }

        public override DbType DbType
        {
            get { return pDbType; }
            set { pDbType = value; }
        }

        public override ParameterDirection Direction
        {
            get { return pDirection; }
            set { pDirection = value; }
        }

        public override bool IsNullable
        {
            get { return pIsNullable; }
            set { pIsNullable = value; }
        }

        public override string ParameterName
        {
            get { return pName; }
            set { pName = value; }
        }

        public override int Size
        {
            get { return pSize; }
            set { pSize = value; }
        }

        public override string SourceColumn
        {
            get { return pSourceColumn; }
            set { pSourceColumn = value; }
        }

        public override bool SourceColumnNullMapping
        {
            get { return pSourceColumnNullMapping; }
            set { pSourceColumnNullMapping = value; }
        }

        public override object Value
        {
            get { return pValue; }
            set { pValue = value; }
        }

        public override void ResetDbType()
        {
            pDbType = DbType.Object;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public DbParameter Parameter
        {
            get
            {
                return (DbParameter)base.MemberwiseClone();
                //return (DbParameter)this.Clone();
            }
        }
    }
}
