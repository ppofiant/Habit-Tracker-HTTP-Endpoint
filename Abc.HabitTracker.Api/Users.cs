using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Abc.HabitTracker.Api
{
    // template for userID, will be using this class for testing api
    public class Users
    {
        public Guid ID { set; get; }
        public string Name { set; get; }
    }
}