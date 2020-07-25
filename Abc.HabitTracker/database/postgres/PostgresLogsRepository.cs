using System;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;

namespace Abc.HabitTracker.Database.Postgres
{
    public class PostgresLogsRepository : ILogsRepository
    {

        NpgsqlConnection _connection;
        NpgsqlTransaction _transaction;

        public PostgresLogsRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Logs FindByHabitIdAndUserId(Guid habit_id, Guid user_id)
        {
            int count = 0;
            List<DateTime> date = new List<DateTime>();
            string query = @"select count(1), created_at from ""logs"" where habit_id = @habit_id and user_id = @user_id and deleted_at is null group by created_at order by created_at ASC";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                        date.Add(reader.GetDateTime(1));
                    }
                    reader.Close();
                }
            }
            return new Logs(count, date);
        }

        public void AddLogs(Track habitTrack)
        {
            HabitTrack ht = habitTrack as HabitTrack;
            if (ht == null) throw new Exception("Tracking is null");

            string query = @"insert into ""logs"" (id, habit_id, user_id, created_at) values(@id, @habit_id, @user_id, @created_at)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("habit_id", ht.Todo.ID);
                cmd.Parameters.AddWithValue("user_id", ht.Person);
                cmd.Parameters.AddWithValue("created_at", ht.Date);

                cmd.ExecuteNonQuery();
            }
        }

        private int getCountSnapshot(Guid habit_id, Guid user_id)
        {
            NpgsqlDateTime lastCreatedDate = new NpgsqlDateTime(0);
            int count = 0;
            string query = @" select created_at, countlogs from ""logs_snapshot"" where habit_id = @habit_id and user_id = @user_id order by created_at DESC LIMIT 1 ";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);

                using(NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        lastCreatedDate = reader.GetDateTime(0);
                        count = reader.GetInt32(1);
                    }
                }
                if(count % 100 == 0)
                {
                    CreateSnapShotLogs(habit_id, user_id);
                }
            }

            query = @"select created_at, coalesce(count(1), 0) from ""logs"" where habit_id = @habit_id and user_id = @user_id and created_at > @last_created";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("last_created", lastCreatedDate);

                using(NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int c = reader.GetInt32(1);
                        count += c;

                    }
                    reader.Close();
                }
            }
            return count;
        }

        public void CreateSnapShotLogs(Guid habit_id, Guid user_id)
        {
            Guid lastLogsId;
            NpgsqlDateTime lastCreatedAt;
            string query = @"select id, created_at from ""logs"" where habit_id = @habit_id 
            and user_id = @user_id ORDER BY created_at DESC LIMIT 1";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);

                using(NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        lastLogsId = reader.GetGuid(0);
                        lastCreatedAt = reader.GetDateTime(1);
                    }

                    else
                    {
                        throw new Exception("last logs not found");
                    }
                }
            }
            int count = getCountSnapshot(user_id, habit_id);

            query = @" insert into logs_snapshot (id, habit_id, user_id, last_logs_id, countlogs, last_logs_created_at) values(@id, @habit_id, @user_id, @last_logs_id, @last_logs_created_at)";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("last_logs_id", lastLogsId);
                cmd.Parameters.AddWithValue("last_logs_created_at", lastCreatedAt);

                cmd.ExecuteNonQuery();
            }
        }
    }
}