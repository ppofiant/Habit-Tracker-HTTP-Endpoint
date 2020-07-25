using System;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;

namespace Abc.HabitTracker.Database.Postgres
{
    public class PostgresHabitRepository : IHabitRepository
    {
        NpgsqlConnection _connection;
        NpgsqlTransaction _transaction;

        public PostgresHabitRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Habit FindById(Guid id)
        {
            string query1 = @"select coalesce(sum(log), 0), created_at from ""logs"" where habit_id = @id and deleted at is not null";
            int countlogs = 0;
            List<DateTime> getArrdate = new List<DateTime>();

            using (var cmd = new NpgsqlCommand(query1, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countlogs = reader.GetInt32(0);
                        getArrdate.Add(reader.GetDateTime(1));
                    }
                    reader.Close();
                }
            }

            string query = @"select name, user_id, CAST(days_off as text) from ""habit"" where id = @id and deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString(0);
                        Guid user_id = reader.GetGuid(1);
                        string daysoff = reader.GetString(3);

                        string[] dday = daysoff.Split(new Char[] { '{', '}', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int index = 0; index < dday.Length; index++)
                        {
                            dday[index] = dday[index].Trim();
                        }

                        Habit h = new Habit(id, user_id, name, dday, new Logs(countlogs, getArrdate));
                    }
                }
                return null;
            }
        }

        public void Create(Habit habit)
        {
            string text = ConvertToString(habit.DayOff);

            string query = @"insert into ""habit"" (id, name, user_id, days_off) values(@id, @name, @user_id, ARRAY[@days_off])";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", habit.ID);
                cmd.Parameters.AddWithValue("name", habit.Name);
                cmd.Parameters.AddWithValue("user_id", habit.UserId);
                cmd.Parameters.AddWithValue("days_off", text);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Habit> FindByUserId(Guid user_id)
        {
            List<Habit> _list = new List<Habit>();
            string query = @"select id, name, user_id, CAST(days_off as text) from ""habit"" where user_id = @user_id and deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("user_id", user_id);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid id = reader.GetGuid(0);
                        string name = reader.GetString(1);
                        Guid _user_id = reader.GetGuid(2);
                        string daysoff = reader.GetString(3);

                        string[] dday = daysoff.Split(new Char[] { '{', '}', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int index = 0; index < dday.Length; index++)
                        {
                            dday[index] = dday[index].Trim();
                        }

                        Habit h = new Habit(id, _user_id, name, dday, new Logs());
                        _list.Add(h);
                    }
                }
                return _list;
            }
        }

        public Habit FindByIdAndUserId(Guid id, Guid user_id)
        {
            // string query1 = @"select count(1), id, created_at from ""logs"" where user_id = @id and habit_id = @habit_id and deleted_at is null GROUP BY id ORDER BY created_at ASC";
            // int countlogs = 0;
            // List<DateTime> getArrdate = new List<DateTime>();

            // using (var cmd = new NpgsqlCommand(query1, _connection, _transaction))
            // {
            //     cmd.Parameters.AddWithValue("id", user_id);
            //     cmd.Parameters.AddWithValue("habit_id", id);

            //     using (NpgsqlDataReader reader = cmd.ExecuteReader())
            //     {
            //         while (reader.Read())
            //         {
            //             countlogs = reader.GetInt32(0);
            //             getArrdate.Add(reader.GetDateTime(2));
            //         }
            //         reader.Close();
            //     }
            // }


            Habit h = null;
            string query = @"select name, CAST(days_off as text) from ""habit"" where id = @id and user_id = @user_id and deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("user_id", user_id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString(0);
                        string daysoff = reader.GetString(1);

                        string[] dday = daysoff.Split(new Char[] { '{', '}', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int index = 0; index < dday.Length; index++)
                        {
                            dday[index] = dday[index].Trim();
                        }

                        h = new Habit(id, user_id, name, dday, new Logs());
                        return h;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void UpdateHabit(Habit habit, string name, string[] off = null)
        {
            string text;
            if (off == null)
            {
                text = ConvertToString(habit.DayOff);
            }
            else
            {
                text = ConvertToString(off);
            }
            string query = @"update habit set name = @name, days_off = ARRAY[@days_off], updated_at = CURRENT_TIMESTAMP where id = @id and deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("days_off", text);
                cmd.Parameters.AddWithValue("id", habit.ID);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHabit(Habit habit)
        {
            string query = @"update ""habit"" set deleted_at = CURRENT_TIMESTAMP where id = @id and deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", habit.ID);

                cmd.ExecuteNonQuery();
            }
        }

        private string ConvertToString(string[] days)
        {
            int size = 0;
            string text = null;
            foreach (var i in days)
            {
                size++;
                if (size == days.Length)
                {
                    text = text + i;
                }
                else
                {
                    text = text + i + ",";
                }
            }
            return text;
        }
    }
}