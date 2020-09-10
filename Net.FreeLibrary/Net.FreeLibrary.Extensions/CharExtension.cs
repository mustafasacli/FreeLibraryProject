using System;

namespace Net.FreeLibrary.Extensions
{
    public static class CharExtension
    {

        #region [ Char2Int method ]

        public static Int32 Char2Int(this char ch)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ch);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        #endregion

    }
}
