using System;

namespace FreeLibrary.Entity.Extensions
{
    public static class BaseBOExtension
    {
        public static bool IsNullOrDefault<T>(this T t) where T : IBaseBO, new()
        {
            bool result = true;

            if (t == null)
                return result;

            T t2 = Activator.CreateInstance<T>();

            result = t2.Equals(t);

            return result;
        }
    }
}