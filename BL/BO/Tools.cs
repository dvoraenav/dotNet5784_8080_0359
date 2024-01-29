using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T obj)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        StringBuilder sb = new StringBuilder();
        foreach (var property in properties)
        {
            sb.Append(property.Name);
            sb.Append(": ");
            sb.Append(property.GetValue(obj));
            sb.Append(", ");
        }

        // Remove the trailing comma and space
        if (sb.Length > 0)
        {
            sb.Length -= 2;
        }
        //not sure
        return sb.ToString();
    }
}
