using System.Text.Json;
using System.Text.Json.Serialization;

namespace HIPA_BE.Utilities.NumberConverters
{
    public class CustomDoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TryGetDouble(out var result) ? result : Convert.ToDouble(reader.GetDouble());
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}