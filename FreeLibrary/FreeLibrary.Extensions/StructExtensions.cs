namespace FreeLibrary.Extensions
{
    public static class StructExtensions
    {
        public static object GetValue<T>(this T? t) where T : struct
        {
            object val = null;

            if (t.HasValue)
            {
                val = t.Value;
            }

            return val;
        }
    }
}
