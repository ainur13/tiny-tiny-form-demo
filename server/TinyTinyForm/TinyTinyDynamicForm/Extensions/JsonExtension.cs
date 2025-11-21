using System.Text.Json;

namespace TinyTinyDynamicForm;

public static class JsonExtension
{
    public static bool ContainsValue(this JsonElement json, string value)
    {
        switch(json.ValueKind)
        {
            case JsonValueKind.String:
                var str = json.GetString();
                return str != null && str.Contains(value, StringComparison.OrdinalIgnoreCase) ? true : false;
            case JsonValueKind.Object:
                return json.EnumerateObject().Any(x => x.Value.ContainsValue(value));
            case JsonValueKind.Array:
                return json.EnumerateArray().Any(x => x.ContainsValue(value));
            default:
                return false;
        }
    }
}
