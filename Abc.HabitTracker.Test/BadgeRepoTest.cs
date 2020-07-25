using System;
using Xunit;

using Npgsql;

using Abc.HabitTracker.Database.Postgres;

namespace Abc.HabitTracker.Test
{
    public class BadgeRepoTest
    {
        private string _connstring;

        public BadgeRepoTest()
        {
            _connstring = "Host=localhost;Username=postgres;Password=postgres;Database=abc;Port=5432";
        }
        
        [Fact]
        public void CreateBadge()
        {
            NpgsqlConnection _connection= new NpgsqlConnection(_connstring);
            _connection.Open();

            IBadgeRepository repo = new PostgresBadgeRepository(_connection, null);
        }
    }
}