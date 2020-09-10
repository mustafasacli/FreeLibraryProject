using System;
using System.Collections.Generic;
using System.Text;

namespace FreeLibrary.CodeGeneration.Source.BO
{
    internal class Table
    {
        public string NameSpace { get; set; }

        public string TableName { get; set; }

        public string ClassName
        {
            get
            {
                string strResult = string.Empty;

                strResult = string.Format("{0}", TableName);

                strResult = strResult.Trim();
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

                strResult = strResult.TrimEnd('s');

                if (strResult.StartsWith("t_"))
                    strResult = strResult.TrimStart('t').TrimStart('_');

                return strResult;
            }
        }

        public string IdColumn { get; set; }

        public List<Column> TableColumns { get; set; }

        public override string ToString()
        {
            try
            {
                StringBuilder boBuilder = new StringBuilder();
                boBuilder.AppendLine(Constants.USING_Base);
                boBuilder.AppendLine(Constants.USING_BaseBO);
                boBuilder.AppendLine("using System;");
                boBuilder.Append("using ");
                boBuilder.AppendFormat(Constants.DL_NAMESPACE_FORMAT, NameSpace);
                boBuilder.AppendLine(";");
                boBuilder.AppendLine();
                boBuilder.AppendFormat("namespace {0}\n", string.Format(Constants.BO_NAMESPACE_FORMAT, NameSpace));
                boBuilder.AppendLine("{");
                boBuilder.AppendFormat("\tpublic class {0} : {1}\n", TableName.Replace(" ", ""), Constants.BaseBO);
                boBuilder.Append("\t{\n");
                string str;

                foreach (Column col in TableColumns)
                {
                    str = col.ToString();
                    boBuilder.Append(str);
                    str = "";
                }

                boBuilder.AppendFormat("\t\tpublic override string {0}()\n", Constants.GET_TABLE_NAME_METHOD);
                boBuilder.Append("\t\t{\n\t\t\t");
                boBuilder.AppendFormat("return \"{0}\";\n", TableName);
                boBuilder.AppendLine("\t\t}");
                boBuilder.AppendLine();

                boBuilder.AppendFormat("\t\tpublic override string {0}()\n", Constants.GET_ID_COLUMN_METHOD);
                boBuilder.Append("\t\t{\n\t\t\t");
                boBuilder.AppendFormat("return \"{0}\";\n", IdColumn);
                boBuilder.AppendLine("\t\t}");
                boBuilder.AppendLine();
                /*
                boBuilder.AppendLine(MethodString("int", "Insert"));
                boBuilder.AppendLine(MethodString("int", "InsertAndGetId"));
                boBuilder.AppendLine(MethodString("int", "Update"));
                boBuilder.AppendLine(MethodString("int", "Delete"));
                */
                /*
                boBuilder.AppendLine("\t\tpublic int Insert()\n\t\t{");
                // starting try block
                boBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
                boBuilder.AppendFormat("\t\t\t\treturn (new {0}DL(this)).Insert();\n", TableName.Trim());
                // ending try block
                boBuilder.AppendLine("\t\t\t}");
                // starting-ending catch block
                boBuilder.AppendLine("\t\t\tcatch\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
                boBuilder.AppendLine("\t\t}\n");
               
                boBuilder.AppendLine("\t\tpublic int InsertAndGetId()\n\t\t{");
                // starting try block
                boBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
                boBuilder.AppendFormat("\t\t\t\treturn (new {0}DL(this)).InsertAndGetId();\n", TableName.Trim());
                // ending try block
                boBuilder.AppendLine("\t\t\t}");
                // starting-ending catch block
                boBuilder.AppendLine("\t\t\tcatch\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
                boBuilder.AppendLine("\t\t}\n");

                boBuilder.AppendLine("\t\tpublic int Update()\n\t\t{");
                // starting try block
                boBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
                boBuilder.AppendFormat("\t\t\t\treturn (new {0}DL(this)).Update();\n", TableName.Trim());
                // ending try block
                boBuilder.AppendLine("\t\t\t}");
                // starting-ending catch block
                boBuilder.AppendLine("\t\t\tcatch\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
                boBuilder.AppendLine("\t\t}\n");

                boBuilder.AppendLine("\t\tpublic int Delete()\n\t\t{");
                // starting try block
                boBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
                boBuilder.AppendFormat("\t\t\treturn (new {0}DL(this)).Delete();\n", TableName.Trim());
                // ending try block
                boBuilder.AppendLine("\t\t\t}");
                // starting-ending catch block
                boBuilder.AppendLine("\t\t\tcatch\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
                boBuilder.AppendLine("\t\t}\n");
                */

                boBuilder.AppendLine("\t}");
                boBuilder.Append("}");
                return boBuilder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string ToDLString()
        {
            try
            {
                StringBuilder dlBuilder = new StringBuilder();
                dlBuilder.AppendLine("using System;");
                dlBuilder.AppendLine(string.Format("{0}", Constants.USING_Base));
                dlBuilder.AppendLine(string.Format("{0}", Constants.USING_BaseDL)).AppendLine();
                dlBuilder.AppendFormat("namespace {0}\n", string.Format(Constants.DL_NAMESPACE_FORMAT, NameSpace));
                dlBuilder.AppendLine("{");

                dlBuilder.AppendFormat("\tinternal class {0}DL : {1}\n", TableName.Replace(" ", ""), Constants.BaseDL);
                dlBuilder.AppendLine("\t{");

                dlBuilder.AppendFormat("\t\tinternal {0}DL()\n", TableName.Replace(" ", ""));
                dlBuilder.AppendLine("\t\t\t: base()");
                dlBuilder.AppendLine("\t\t{\n\t\t}");

                dlBuilder.Append("\t}\n}");
                return dlBuilder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string MethodString(string returnType, string methodName)
        {
            StringBuilder mthdBuilder = new StringBuilder();

            mthdBuilder.AppendFormat("\t\tinternal {0} {1}()\n\t\t", returnType, methodName);

            mthdBuilder.AppendLine("{");
            // starting try block
            mthdBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
            mthdBuilder.AppendFormat("\t\t\t\tusing({0}DL _{1}dlDL = new {0}DL())\n", TableName.Replace(" ", ""), TableName.Replace(" ", "").ToLower().Replace("ı", "i"));
            mthdBuilder.AppendLine("\t\t\t\t{");
            mthdBuilder.AppendFormat("\t\t\t\t\treturn _{0}dlDL.{1}(this);\n", TableName.Replace(" ", "").ToLower().Replace("ı", "i"), methodName);
            mthdBuilder.AppendLine("\t\t\t\t}");
            // ending try block
            mthdBuilder.AppendLine("\t\t\t}");
            // starting-ending catch block
            mthdBuilder.AppendLine("\t\t\tcatch (Exception)\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
            mthdBuilder.AppendLine("\t\t}");

            return mthdBuilder.ToString();
        }
    }
}
