using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Abc.HabitTracker;

namespace Abc.HabitTracker.Api
{
    public class RequestData
    {

        [JsonPropertyName("name")]
        public String Name { get; set; }

        [JsonPropertyName("days_off")]
        public String[] DaysOff { get; set; }

        [JsonPropertyName("updated_name")]
        public String UpdateName { get; set; }

        [JsonPropertyName("updated_days_off")]
        public String[] UpdateDaysOff { get; set; }
    }
}
