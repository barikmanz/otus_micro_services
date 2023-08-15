using Newtonsoft.Json;

namespace UserService.Infrastructure.SeedWork.JsonSerializers
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Format: Days.Hours:Minutes:Seconds
        /// </summary>
        public const string TimeSpanFormatString = @"d\.hh\:mm\:ss";

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteValue(timespanFormatted);
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            TimeSpan parsedTimeSpan;

            string value = (string) reader.Value;
            if (!value.Contains("."))
            {
                string[] parts = value.Split(':');
                int hours = int.Parse(parts[0]);
                int minutes = int.Parse(parts[1]);
                int seconds = int.Parse(parts[2]);

                parsedTimeSpan = new TimeSpan(hours, minutes, seconds);
            }
            else
              TimeSpan.TryParseExact(value, TimeSpanFormatString, null, out parsedTimeSpan);
            return parsedTimeSpan;
        }
    }
}
