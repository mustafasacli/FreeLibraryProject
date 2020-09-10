using System;

namespace Net.FreeLibrary.Extensions
{
    public static class DatetimeExtension
    {

        #region [ LastTimeOfDay method ]

        public static DateTime LastTimeOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        #endregion

        #region [ FirstTimeOfDay method ]

        public static DateTime FirstTimeOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        #endregion

    }
}
