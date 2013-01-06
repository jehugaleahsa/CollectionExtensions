using System;
using CollectionExtensions.Properties;

namespace CollectionExtensions
{
    internal static class Converter<TResult>
    {
        private static readonly Type _type = getType();

        private static Type getType()
        {
            Type type = typeof(TResult);
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                type = underlyingType;
            }
            return type;
        }

        public static TResult Convert(object value, IFormatProvider provider)
        {
            if (value == null)
            {
                if (_type == typeof(TResult) && typeof(TResult).IsValueType)
                {
                    throw new InvalidCastException(Resources.NullValueType);
                }
                return default(TResult);
            }
            if (value is IConvertible)
            {
                return (TResult)System.Convert.ChangeType(value, _type, provider);
            }
            else
            {
                return (TResult)value;
            }
        }
    }
}
