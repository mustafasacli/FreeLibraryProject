using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using FreeLibrary.Extensions;

namespace FreeLibrary.DataAccess.Extensions
{
    internal static class DataAccessExtension
    {
        #region [ DataTable To Generic List ]

        /// <summary>
        /// This method returns A List of T object.
        /// </summary>
        /// <typeparam name="T">T object type</typeparam>
        /// <param name="datatable">Datatble object</param>
        /// <param name="accordingToColumn">if it is true, returns a List with DataTable Columns else returns a List with PropertyInfo of Object.</param>
        /// <returns>Returns A List of T object.</returns>
        public static List<T> ToList<T>(this DataTable datatable, bool accordingToColumn) where T : new()
        {
            try
            {
                List<T> liste = new List<T>();
                object obj;
                T item = Activator.CreateInstance<T>();
                if (accordingToColumn == true)
                {
                    PropertyInfo propInfo = null;
                    foreach (DataRow row in datatable.Rows)
                    {
                        item = Activator.CreateInstance<T>();
                        foreach (DataColumn col in datatable.Columns)
                        {
                            obj = row[col.ColumnName];
                            if (obj.IsNullOrDbNull() == false)
                            {
                                propInfo = typeof(T).GetProperty(col.ColumnName);
                                propInfo.SetValue(item, obj);
                            }
                        }

                        liste.Add(item);
                    }
                }
                else
                {
                    PropertyInfo[] pInfos = typeof(T).GetProperties();
                    foreach (DataRow row in datatable.Rows)
                    {
                        item = Activator.CreateInstance<T>();
                        for (int proCounter = 0; proCounter < pInfos.Length; proCounter++)
                        {
                            obj = row[pInfos[proCounter].Name];
                            if (obj.IsNullOrDbNull() == false)
                                pInfos[proCounter].SetValue(item, obj);
                        }
                        liste.Add(item);
                    }
                }
                return liste;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion [ DataTable To Generic List ]
    }
}
