using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace HealthMed.Domain.Helpers;

public static class ParseHelper
{
    public static bool ToBool(this string input)
    {
        bool data = false;
        bool.TryParse(input, out data);
        return data;
    }

    public static decimal ToDecimal(this string input)
    {
        decimal data = 0;
        decimal.TryParse(input, out data);
        return data;
    }
    public static double ToDouble(this string input)
    {
        double data = 0;
        double.TryParse(input, out data);
        return data;
    }

    public static object GetPropertyValue(this object @object, string propertyName)
    {
        return @object.GetType().GetProperty(propertyName)?.GetValue(@object, null);
    }

    public static bool TryCast<T>(this object obj, out T result)
    {
        if (obj is T)
        {
            result = (T)obj;
            return true;
        }

        result = default(T);
        return false;
    }

    public static string ToJson(this object obj)
    {
        if (obj == null) return string.Empty;
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }

    public static long ToInt64(this object obj)
    {
        if (obj != null)
            if (obj is double)
                return Convert.ToInt64(obj);

        Int64.TryParse(obj?.ToString(), out long result);

        return result;
    }

    public static int ToInt32(this object obj)
    {
        if (obj != null)
            if (obj is double)
                return Convert.ToInt32(obj);

        Int32.TryParse(obj?.ToString(), out int result);

        return result;
    }

    public static DateTime? ToDateTime(this object obj)
    {
        string dateString = obj?.ToString();
        // Verifica se a string está no formato específico "D:yyyyMMddHHmmsszzz"
        if (dateString.StartsWith("D:") && dateString.Length == 23)
            dateString = $"{dateString[2..6]}-{dateString[6..8]}-{dateString[8..10]}T{dateString[10..12]}:{dateString[12..14]}:{dateString[14..16]}";

        DateTime.TryParse(dateString, out var result);

        return result == DateTime.MaxValue || result == DateTime.MinValue ? default(DateTime?) : result;
    }

    public static string MatchReplace(this string input, string pattern, string replacement)
    {
        return Regex.Replace(input, pattern, replacement);
    }

    public static List<T> CopyToList<T>(this IList<T> data)
    {
        List<T> list = (List<T>)Activator.CreateInstance<List<T>>();

        foreach (var r in data)
        {
            var item = r.CopyTo<T>();

            if (item != null)
                list.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new object[] { item });
        }
        return list;
    }

    public static T CopyTo<T>(this object source)
    {

        //if (item == null)
        T item = (T)Activator.CreateInstance(typeof(T));

        if (item != null)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                PropertyInfo sourceAttr = source.GetType().GetProperty(prop.Name);

                if (sourceAttr != null && prop.CanWrite)
                    prop.SetValue(item, Convert.ChangeType(sourceAttr.GetValue(source, null), prop.PropertyType), null);
            }
        }

        return item;
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
