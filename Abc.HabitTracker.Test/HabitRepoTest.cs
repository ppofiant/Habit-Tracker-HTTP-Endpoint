using System;
using Xunit;
using Npgsql;

using Abc.HabitTracker.Database.Postgres;

namespace Abc.HabitTracker.Test
{
    public class HabitRepoTest
    {
        private string _connstring;

        public HabitRepoTest()
        {
            _connstring = "Host=localhost;Username=postgres;Password=postgres;Database=abc;Port=5432";
        }

        [Fact]
        public void CreateTest()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(_connstring);
            _connection.Open();
        }
    }
}