using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.FreeLibrary.Extensions
{
    public static class StringExtension
    {
        #region [ Str2Int method ]

        public static Int32 Str2Int(this String str)
        {
            int result = 0;
            try
            {
                result = int.Parse(str);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion [ Str2Int method ]

        #region [ IsNullOrEmpty method ]

        public static Boolean IsNullOrEmpty(this String str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        #endregion [ IsNullOrEmpty method ]

        #region [ IsNullOrSpace method ]

        public static Boolean IsNullOrSpace(this String str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Replace(" ", "").Length == 0;
            }
        }

        #endregion [ IsNullOrSpace method ]

        #region [ FirstIndexOf method ]

        public static int FirstIndexOf(this string str, char ch)
        {
            try
            {
                if (str.IsNullOrEmpty())
                    return -1;

                if (ch.IsNull())
                    return -1;

                char[] chs = str.ToCharArray();
                int _index = -1;
                for (int charCounter = 0; charCounter < chs.Length; charCounter++)
                {
                    if (string.Format("{0}", ch).Equals(string.Format("{0}", chs[charCounter])))
                    {
                        _index = charCounter;
                        break;
                    }
                }
                return _index;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [ FirstIndexOf method ]

        #region [ TrimAll method ]

        public static string TrimAll(this string str)
        {
            string result = null;
            try
            {
                if (str == null)
                    return str;

                if (str.Length == 0)
                    return str;

                result = str.Replace(" ", "");
            }
            catch (Exception)
            {
            }

            return result;
        }

        #endregion [ TrimAll method ]

        #region [ LengthOfString method ]

        public static int LengthOfString(this string str)
        {
            int result = -1;
            try
            {
                if (str == null)
                    return -1;

                result = str.Length;
            }
            catch (Exception)
            {
            }

            return result;
        }

        #endregion [ LengthOfString method ]

        #region [ IsEqual method ]

        public static bool IsEqual(this string str1, string str2)
        {
            bool result = false;
            try
            {
                if (str1 == null && str2 == null)
                {
                    result = true;
                    return result;
                }

                if (str1 != null && str2 != null)
                {
                    result = string.Equals(str1, str2);
                    return result;
                }

                result = false;
            }
            catch (Exception)
            {
            }

            return result;
        }

        #endregion [ IsEqual method ]

        #region [ Split2List method ]

        public static List<string> Split2List(this string text, char splitChar)
        {
            List<string> splitResult = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(text) == false)
                {
                    string[] strArr = text.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);

                    if (strArr != null)
                    {
                        splitResult = strArr.ToList<string>();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (splitResult == null)
            {
                splitResult = new List<string>();
            }

            return splitResult;
        }

        #endregion [ Split2List method ]

        #region [ ConcatListWithAnString method ]

        public static string ConcatListWithAnString(this List<string> list, string str, bool AppendJustOneItem)
        {
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                if (list != null)
                {
                    if (list.Count > 1)
                    {
                        for (int counter = 0; counter < list.Count - 1; counter++)
                        {
                            strBuilder.Append(string.Format("{0}{1}", list[counter], str));
                        }

                        strBuilder.Append(list[list.Count - 1]);
                    }
                    else
                    {
                        if (list.Count == 1)
                        {
                            strBuilder.Append(string.Format("{0}{1}", list[0], AppendJustOneItem ? str : ""));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            string result = strBuilder.ToString();
            return result;
        }

        #endregion [ ConcatListWithAnString method ]
    }
}