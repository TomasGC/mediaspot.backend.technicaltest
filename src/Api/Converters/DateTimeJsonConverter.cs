using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mediaspot.Api.Converters;

/// <summary>
/// Convert DateTime into a correct json format 
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public const string FORMAT = "s";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return DateTime.ParseExact(reader.GetString(), FORMAT, new DateTimeFormatInfo());
        }
        catch (Exception e)
        {
            throw new JsonException(nameof(DateTimeJsonConverter) + " Cannot convert to DateTime", e);
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(FORMAT));
    }
}
