using System;
using System.Collections.Generic;

namespace Net.FreeLibrary.Extensions
{
    public static class ListExtension
    {
        #region [ Page method ]

        public static List<T> Page<T>(this List<T> list, int page, int pageItemCount) where T : new()
        {
            try
            {
                if (list == null)
                    return null;

                if (page < 0)
                    throw new Exception("page Number can not be less than zero.");

                if (pageItemCount < 1)
                    throw new Exception("page Item Count can not be less than one.");

                List<T> lstNew = new List<T>();

                int totalCount = list.Count;

                if (totalCount == 0)
                {
                    return lstNew;
                }

                int startIndex = page * pageItemCount;

                int endIndex = (page + 1) * pageItemCount;

                startIndex = startIndex > totalCount ? totalCount : startIndex;
                endIndex = endIndex > totalCount ? totalCount : endIndex;

                int count = endIndex - startIndex;

                for (int counter = 0; counter < count; counter++)
                {
                    lstNew.Add(list[startIndex + counter]);
                }

                return lstNew;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}