using System;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Enum Helper
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Get description attribute of enum item
    /// </summary>
    /// <param name="value">The enum item</param>
    /// <returns>string</returns>
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        MemberInfo[] memberInfo = type.GetMember(value.ToString());
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        return value.ToString();
    }

}
