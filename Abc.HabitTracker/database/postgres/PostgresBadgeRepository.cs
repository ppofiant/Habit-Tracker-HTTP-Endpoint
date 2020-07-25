using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace Abc.HabitTracker.Database.Postgres
{
    public class PostgresBadgeRepository : IBadgeRepository
    {
        NpgsqlConnection _connection;
        NpgsqlTransaction _transaction;

        public PostgresBadgeRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Badge FindById(Guid id)
        {
            string query = @"select name, description from ""badge"" where id = @id AND deleted_at is null";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                using(NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        string name = reader.GetString(0);
                        string description = reader.GetString(1);

                        Badge b = new Badge(id, name, description);
                        return b;
                    }
                }
            }
            return null;
        }

        public void CreateBadge(Badge badge, DateTime date_created)
        {
            string query = @"INSERT INTO ""badge""(id, name, description, user_id, created_at) VALUES(@id, @name, @description, @user_id, @created_at)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("user_id", badge.UserId);
                cmd.Parameters.AddWithValue("name", badge.Name);
                cmd.Parameters.AddWithValue("description", badge.Description);
                cmd.Parameters.AddWithValue("created_at", date_created);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Badge> FindByUserId(Guid user_id)
        {
            List<Badge> _listbadge = new List<Badge>();
            string query = @"select id, name, description, created_at from ""badge"" where user_id = @user_id and deleted_at is null order by created_at ASC";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("user_id", user_id);

                using(NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Guid id = reader.GetGuid(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        DateTime created = reader.GetDateTime(3);

                        Badge founded_badge = new Badge(id, name, description, created);
                        _listbadge.Add(founded_badge);
                    }
                }
                return _listbadge;
            }
        }
    }
}