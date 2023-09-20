using System.ComponentModel;
using lib.Lab.Models.Enum;

namespace lib.Lab.Provider;

public static class EnumExtension
{
    public static string Description(this Enum @enum)
    {
        var enumType = @enum.GetType();
        var field = enumType.GetField(@enum.ToString());
        var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        
        return attributes?.Length == 0 ? @enum.ToString() : ((DescriptionAttribute)attributes[0]).Description;
    }
    
    public static string GetNameEnumLab(this Labs @lab) => Enum.GetName(typeof(Labs), @lab);
}