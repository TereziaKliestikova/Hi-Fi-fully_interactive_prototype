using log4net.Core;
using System.Text.Json.Serialization;
using log4net.Layout;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HIPA_BE.Logger
{
    public class JsonLayout : LayoutSkeleton
    {
        private static readonly AsyncLocal<string> ContextId = new();

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            // Causes serializer to not sanitize some special characters
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        private static readonly int _maxLogLength = 10000;

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(loggingEvent);

            if (ContextId.Value == default)
            {
                ContextId.Value = Guid.NewGuid().ToString();
            }

            var data = new Container
            {
                Timestamp = loggingEvent.TimeStamp.ToUniversalTime(),
                Level = loggingEvent.Level?.DisplayName ?? "None",
                //ContextId = GlobalContext.Properties["ContextId"]?.ToString() ?? "Unknown",
                ContextId = ContextId?.Value ?? "",
                ThreadId = Environment.CurrentManagedThreadId,
                // TODO: Test behaviour with non-string MessageObjects and whether formatting is weird, etc
                Message = loggingEvent.RenderedMessage ?? "",
                Exception = loggingEvent.ExceptionObject?.ToString() ?? "",
                Caller = loggingEvent.LocationInformation?.FullInfo.ToString() == "?" ? ""
                : loggingEvent.LocationInformation?.FullInfo.ToString() ?? ""
            };

            string data_json;
            try
            {
                data_json = JsonSerializer.Serialize(data, _serializerOptions);


                if (data_json.Length > _maxLogLength)
                {
                    throw new Exception($"Maximum log length of {_maxLogLength} exceeded. Actual log excerpt: {data_json[0.._maxLogLength]}");
                }
            }
            catch (Exception e)
            {
                data_json = $"Failed to serialize to json when logging. Exception: {e}";
            }

            writer.WriteLine(data_json);
        }

        public override void ActivateOptions()
        {
            throw new NotImplementedException();
        }

        private class Container
        {
            [JsonPropertyName("@t")]
            public required DateTime Timestamp { get; set; }

            [JsonPropertyName("@l")]
            public required string Level { get; set; }

            [JsonPropertyName("@c")]
            public string ContextId { get; set; } = "";

            public int ThreadId { get; set; }

            [JsonPropertyName("@m")]
            public object Message { get; set; } = "";

            [JsonPropertyName("@x")]
            public string Exception { get; set; } = "";

            public string Caller { get; set; } = "";
        }
    }
}
