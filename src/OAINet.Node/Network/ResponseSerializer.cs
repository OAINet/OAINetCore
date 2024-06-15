using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public static class ResponseSerializer
{
    public static string Serialize(object obj)
    {
        var sb = new StringBuilder();
        SerializeObject(obj, sb, 0);
        return sb.ToString();
    }

    private static void SerializeObject(object obj, StringBuilder sb, int indentLevel)
    {
        if (obj == null)
        {
            return;
        }

        var type = obj.GetType();
        var properties = type.GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);
            SerializeProperty(prop.Name, value, sb, indentLevel);
        }
    }

    private static void SerializeProperty(string name, object value, StringBuilder sb, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        if (value is IEnumerable && !(value is string))
        {
            sb.AppendLine($"{indent}{name}:");
            foreach (var item in (IEnumerable)value)
            {
                SerializeObject(item, sb, indentLevel + 1);
                sb.AppendLine($"{indent};");
            }
        }
        else
        {
            sb.AppendLine($"{indent}{name}: {value};");
        }
    }
}