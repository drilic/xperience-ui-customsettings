using System.Reflection;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Utility
{
    public static class CustomChannelSettingsReflectionHelper
    {
        public static IEnumerable<PropertyInfo>? GetAllPropertiesWithAttribute(Type objectType, Type attributeType)
                        => objectType?.GetProperties()?.Where(prop => prop.IsDefined(attributeType, false));

        public static void SetPropertyValue(object obj, string propertyName, object val)
        => obj?.GetType()?.GetProperty(propertyName)?.SetValue(obj, val);

        public static object? GetPropertyValue(object obj, string propertyName)
            => obj?.GetType()?.GetProperty(propertyName)?.GetValue(obj);
    }
}
