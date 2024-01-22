using System;
using System.Reflection;
using System.ComponentModel;
using lib.Lab.Models.Enum;

namespace lib.Lab.Providers;

public static class EnumExtension
{
    
    public static string Description(this Enum @enum)
    {
        FieldInfo? fieldInfo = @enum.GetType().GetField(@enum.ToString());
        var attribute = fieldInfo?.GetCustomAttribute<LabDataAttribute>();
        
        return attribute?.Description ?? "none";
    }

    public static bool GetIsInclude(this Enum @enum)
    {
        FieldInfo? fieldInfo = @enum.GetType().GetField(@enum.ToString());
        var attribute = fieldInfo?.GetCustomAttribute<LabDataAttribute>();
        
        return attribute?.IsInclude ?? false;
    }
    
    
    public static string GetNameEnumLab(this Labs @lab) => Enum.GetName(typeof(Labs), @lab);
    public static Labs GetParseName(string name) => (Labs)Enum.Parse(typeof(Labs), name);
}