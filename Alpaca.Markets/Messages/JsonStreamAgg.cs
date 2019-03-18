﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Alpaca.Markets
{
    internal sealed class JsonStreamAgg : IStreamAgg
    {
        [JsonProperty(PropertyName = "sym", Required = Required.Always)]
        public String Symbol { get; set; }

        [JsonProperty(PropertyName = "x", Required = Required.Default)]
        public Int64 Exchange { get; set; }

        [JsonProperty(PropertyName = "o", Required = Required.Always)]
        public Decimal Open { get; set; }

        [JsonProperty(PropertyName = "h", Required = Required.Always)]
        public Decimal High { get; set; }

        [JsonProperty(PropertyName = "l", Required = Required.Always)]
        public Decimal Low { get; set; }

        [JsonProperty(PropertyName = "c", Required = Required.Always)]
        public Decimal Close { get; set; }

        [JsonProperty(PropertyName = "a", Required = Required.Always)]
        public Decimal Average { get; set; }

        [JsonProperty(PropertyName = "v", Required = Required.Always)]
        public Int64 Volume { get; set; }

        [JsonProperty(PropertyName = "s", Required = Required.Always)]
        public Int64 StartTimeOffset { get; set; }

        [JsonProperty(PropertyName = "e", Required = Required.Always)]
        public Int64 EndTimeOffset { get; set; }

        [JsonIgnore]
        public DateTime StartTime { get; set; }

        [JsonIgnore]
        public DateTime EndTime { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(
            StreamingContext context)
        {
#if NET45
            StartTime = DateTimeHelper.FromUnixTimeMilliseconds(StartTimeOffset);
            EndTime = DateTimeHelper.FromUnixTimeMilliseconds(EndTimeOffset);
#else
            StartTime = DateTime.SpecifyKind(
                DateTimeOffset.FromUnixTimeMilliseconds(StartTimeOffset)
                    .DateTime, DateTimeKind.Utc);

            EndTime = DateTime.SpecifyKind(
                DateTimeOffset.FromUnixTimeMilliseconds(StartTimeOffset)
                    .DateTime, DateTimeKind.Utc);
#endif
        }
    }
}