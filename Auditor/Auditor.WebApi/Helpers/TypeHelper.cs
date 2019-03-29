using System;

namespace Auditor.WebApi.Helpers
{
    internal static class TypeHelper
    {
        public static bool IsNullOrDefault(Type type, object value)
        {
            if (value == null)
                return true;

            if (type.IsValueType)
                return value.Equals(Activator.CreateInstance(type));
            
            return false;
        }
    }
}
