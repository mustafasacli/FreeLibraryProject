using System.Text;

namespace FreeLibrary.CodeGeneration.Source.BO
{
    internal class Column
    {
        private string _propName;
        private int _isRequired;
        private int _stringMinLen;
        private int? _stringMaxLen;

        internal string PropertyChangeMethodName
        {
            get { return "OnPropertyChanged"; }
        }
        /*
        public Column(string sColumnName, string sColumnType, bool sIsRequired = false)
        {
            _ColumnName = sColumnName;
            _ColumnTypeName = sColumnType;
            _propName = string.Empty;
            _isRequired = sIsRequired;
            _stringMinLen = 0;
            _stringMaxLen = 50;
        }
        */

        private string _ColumnName;

        public string ColumnName
        {
            get { return _ColumnName; }
            set
            {
                _ColumnName = value;
                _propName = PropName();
            }
        }

        public string PropertyName
        {
            get
            {
                //_propName = PropName();
                return _propName;
            }
        }

        private string PropName()
        {
            string strResult = string.Empty;

            strResult = string.Format("{0}", _ColumnName);

            strResult = strResult.Replace(" ", "");
            strResult = strResult.Replace("ğ", "g");
            strResult = strResult.Replace("ı", "i");
            strResult = strResult.Replace("ç", "c");
            strResult = strResult.Replace("ö", "o");
            strResult = strResult.Replace("ü", "u");
            strResult = strResult.Replace("Ğ", "G");
            strResult = strResult.Replace("Ç", "C");
            strResult = strResult.Replace("Ö", "O");
            strResult = strResult.Replace("Ü", "U");
            strResult = strResult.Replace("İ", "I");

            return strResult;
        }

        public string DataType { get; set; }

        private string _ColumnTypeName;

        public string ColumnTypeName
        {
            get { return this.ConvertFromDbDataType(DataType); }
            set { _ColumnTypeName = value; }
        }

        public int MinimumLength
        {
            get
            {
                return _stringMinLen;
            }

            set
            {
                _stringMinLen = value < 0 ? 0 : value;
            }
        }

        public int? MaximumLength
        {
            get
            {
                return _stringMaxLen;
            }

            set
            {
                _stringMaxLen = value;
            }
        }

        public int IsRequired
        {
            get
            {
                return _isRequired;
            }

            set
            {
                _isRequired = value;
            }
        }

        public int IdentityState { get; set; }

        public object DefaultValue { get; set; }

        public int Precision { get; set; }

        public override string ToString()
        {
            string colName = this.PropertyName;
            string colType = this.ColumnTypeName;

            StringBuilder colBuilder = new StringBuilder();
            colBuilder.AppendFormat("\t\tprivate {0} _{1};\n", colType, colName);

            if (IsRequired == 1)
            {
                if (string.Equals("string", colType))
                {
                    colBuilder.AppendLine("\t\t[Required(AllowEmptyStrings = false)]");

                    if (_stringMaxLen > 0)
                    {
                        colBuilder.AppendFormat("\t\t[StringLength({0}", _stringMaxLen);
                        if (_stringMinLen > 0)
                        {
                            colBuilder.AppendFormat(", MinimumLength = {0}", _stringMinLen);
                        }
                        colBuilder.AppendLine(")]");
                    }
                }
            }

            colBuilder.AppendFormat("\t\tpublic {0} {1}\n", colType, colName);
            colBuilder.Append("\t\t{\n\t\t\tset {");
            colBuilder.AppendFormat(" _{0} = value; {1}(\"{2}\");", colName, PropertyChangeMethodName, colName);
            colBuilder.AppendLine(" }");
            colBuilder.Append("\t\t\tget {");
            colBuilder.AppendFormat(" return _{0}; ", colName);
            colBuilder.AppendLine("}");
            colBuilder.AppendLine("\t\t}");
            colBuilder.AppendLine();
            return colBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (object.ReferenceEquals(obj.GetType(), typeof(Column)))
            {
                Column cl = (Column)obj;
                result = cl.ColumnName.Equals(_ColumnName) &&
                    cl.ColumnTypeName.Equals(_ColumnTypeName);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private string ConvertFromDbDataType(string columnDataType)
        {
            string result = "string";
            string col = string.Format("{0}", columnDataType).ToLower().Replace('ı', 'i');

            switch (col)
            {
                case "int":
                case "integer":
                case "int4":
                    result = "int";
                    break;

                case "tinyint":
                    result = "byte";
                    break;

                case "smallint":
                    result = "short";
                    break;

                case "datetimeoffset":
                case "date":
                case "datetime":
                case "datetime2":
                case "time":
                case "timestamp":
                case "timezone":
                    result = "DateTime";
                    break;

                case "bigint":
                    result = "long";
                    break;

                case "decimal":
                case "smallmoney":
                case "money":
                case "number":
                    result = "decimal";
                    break;

                case "real":
                    result = "float";
                    break;

                case "float":
                    result = "double";
                    break;

                case "bit":
                    result = "bool";
                    break;

                case "nvarchar":
                case "nvarchar2":
                case "varchar":
                case "varchar2":
                case "text":
                case "ntext":
                    result = "string";
                    break;

                case "char":
                case "nchar":
                    result = "char";
                    break;

                case "blob":
                case "binary":
                case "varbinary":
                case "image":
                    result = "byte[]";
                    break;

                default:
                    break;
            }

            if (IsRequired != 1 && !string.Equals(result, "string")
                && !string.Equals(result, "byte[]"))
            {
                result += "?";
            }

            return result;
        }
    }
}