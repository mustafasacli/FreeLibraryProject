using System;

namespace Net.FreeLibrary.Extensions
{
    public static class ObjectExtension
    {

        #region [ IsNull Method ]

        public static Boolean IsNull(this Object o)
        {
            return o == null;
        }

        #endregion

        #region [ IsNullOrDbNull method ]

        public static Boolean IsNullOrDbNull(this Object obj)
        {
            return (null == obj || obj == DBNull.Value);
        }

        #endregion

        #region [ ToStr method ]

        public static String ToStr(this Object obj)
        {
            return obj.IsNullOrDbNull() == true ? String.Empty : obj.ToString();
        }

        #endregion

        #region [ ToBool Method ]

        public static Boolean ToBool(this object obj)
        {
            bool result = false;
            try
            {
                result = bool.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region [ ToByte method ]

        public static byte ToByte(this object obj)
        {
            byte result = 0;
            try
            {
                result = byte.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion

        #region [ ToShort method ]

        public static short ToShort(this object obj)
        {
            short result = 0;
            try
            {
                result = Int16.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion

        #region [ ToInt method ]

        public static Int32 ToInt(this Object obj)
        {
            int result = 0;
            try
            {
                result = obj.ToStr().Str2Int();
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion

        #region [ ToLong method ]

        public static long ToLong(this object obj)
        {
            long result = 0;
            try
            {
                result = long.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion

        #region [ ToSingle method ]

        public static float ToSingle(this object obj)
        {
            float result = .0f;
            try
            {
                result = Single.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = .0f;
            }
            return result;
        }

        #endregion

        #region [ ToDouble method ]

        public static double ToDouble(this object obj)
        {
            double result = .0d;
            try
            {
                result = double.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = .0d;
            }
            return result;
        }

        #endregion

        #region [ ToDecimal method ]

        public static decimal ToDecimal(this object obj)
        {
            decimal result = 0.0M;
            try
            {
                result = decimal.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = 0.0M;
            }
            return result;
        }

        #endregion

        #region [ ToDateTime method ]

        public static DateTime ToDateTime(this object obj)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.Parse(string.Format("{0}", obj));
            }
            catch (Exception)
            {
                result = new DateTime();
            }
            return result;
        }

        #endregion

        #region [ ToDateTime method ]

        public static DateTime ToDateTime(this object obj, string format)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(string.Format("{0}", obj), format, null);
            }
            catch (Exception)
            {
                result = new DateTime();
            }
            return result;
        }

        #endregion

        #region [ ToDateTime method ]

        public static DateTime ToDateTime(this object obj, string format, IFormatProvider formatProvider)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(string.Format("{0}", obj), format, formatProvider);
            }
            catch (Exception)
            {
                result = new DateTime();
            }
            return result;
        }

        #endregion

    }
}