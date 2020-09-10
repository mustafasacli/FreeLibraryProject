using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FreeLibrary.CodeGeneration.Source.BO;

namespace FreeLibrary.CodeGeneration.Source.Printing
{
    internal class Printer
    {
        static string classCrud = "Crud";

        private string _NameSpace = string.Empty;
        public string NameSpace
        {
            get { return _NameSpace; }
            set { _NameSpace = value; }
        }

        private string _savingPath = string.Empty;
        public string SavingPath
        {
            get { return _savingPath; }
            set { _savingPath = value; }
        }

        private string QOToString()
        {
            StringBuilder qoBuilder = new StringBuilder();
            qoBuilder.AppendFormat("namespace {0}.Source.QO\n", this._NameSpace);
            qoBuilder.AppendLine("{");
            qoBuilder.AppendLine("\t/* Query Object Class */");
            qoBuilder.AppendLine("\tpublic class Crud");
            qoBuilder.AppendLine("\t{");
            qoBuilder.AppendLine("\t}");
            qoBuilder.AppendLine("}");
            return qoBuilder.ToString();
        }

        public void PrintClassTable(List<Table> lstClazz)
        {
            try
            {
                if (lstClazz == null || lstClazz.Count == 0)
                    return;

                DirectoryInfo dirInfoSavePath = new DirectoryInfo(_savingPath);

                if (dirInfoSavePath.Exists == false)
                    dirInfoSavePath.Create();

                DirectoryInfo dirInfoSourcePath = dirInfoSavePath.CreateSubdirectory("Source");

                /* Writing Business Object(BO) CLasses Part */

                #region [ Writing Business Object(BO) CLasses Part ]

                string tableStrObject = string.Empty;
                FileInfo fileTableInfo;

                DirectoryInfo dirInfoBO = dirInfoSourcePath.CreateSubdirectory("BO");
                foreach (var clazz in lstClazz)
                {
                    fileTableInfo = new FileInfo(
                        string.Format(@"{0}\{1}.cs",
                        dirInfoBO.FullName,
                        clazz.ClassName.Replace('.', '_')//TableName.Replace(" ", "").Replace('.', '_')
                        ));
                    tableStrObject = clazz.ToString();

                    using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                    {
                        outfile.Write(tableStrObject);
                        outfile.Close();
                    }
                    tableStrObject = string.Empty;
                }

                #endregion

                fileTableInfo = null;

                /* Writing Data Access Layer(DL) Classes Part */

                #region [ Writing Data Layer(DL) Classes Part ]

                DirectoryInfo dirInfoDL = dirInfoSourcePath.CreateSubdirectory("DL");
                foreach (var clazz in lstClazz)
                {
                    fileTableInfo = new FileInfo(string.Format(@"{0}\{1}DL.cs", dirInfoDL.FullName, clazz.TableName.Replace(" ", "").Replace('.', '_')));
                    tableStrObject = clazz.ToDLString();//ClassToDLString(String.Concat(_classNameSpace, ".Source.DL"), clazz);

                    using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                    {
                        outfile.Write(tableStrObject);
                        outfile.Close();
                    }
                    tableStrObject = "";
                }

                #endregion

                fileTableInfo = null;

                /* Writing QO.Crud Class Part */

                #region [ Writing QO.Crud Class Part ]

                DirectoryInfo dirInfQO = dirInfoSourcePath.CreateSubdirectory("QO");
                fileTableInfo = new FileInfo(string.Format(@"{0}\{1}.cs", dirInfQO.FullName, classCrud));
                tableStrObject = QOToString();

                using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                {
                    outfile.Write(tableStrObject);
                    outfile.Close();
                }

                #endregion

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
