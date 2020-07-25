using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Abc.HabitTracker.Api;
using Abc.HabitTracker;
using Abc.HabitTracker.Database.Postgres;
using Npgsql;

namespace Abc.HabitTracker.Api.Controllers
{
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly ILogger<HabitsController> _logger;
        private string _connstringgg;

        public HabitsController(ILogger<HabitsController> logger)
        {
            _logger = logger;
            _connstringgg = "Host=localhost;Username=postgres;Password=postgres;Database=abc;Port=5432";
        }

        [HttpGet("api/v1/users/{userID}/habits")]
        public ActionResult<IEnumerable<Habit>> All(Guid userID)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            IHabitRepository repo = new PostgresHabitRepository(_connection, null);
            List<Habit> _listFounded = repo.FindByUserId(userID);

            if (_listFounded == null) return NotFound("Habit is not founded");

            return _listFounded;
        }

        [HttpGet("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> Get(Guid userID, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            IHabitRepository repo = new PostgresHabitRepository(_connection, null);
            Habit toGet = repo.FindByIdAndUserId(id, userID);

            if (toGet == null) return NotFound("Habit is not founded");

            return toGet;
        }

        [HttpPost("api/v1/users/{userID}/habits")]
        public ActionResult<Habit> AddNewHabit(Guid userID, [FromBody] RequestData data)
        {
            Habit habit = Habit.addNewHabit(userID, data.Name, data.DaysOff);
            if (habit.DayOff == null) return NotFound("Habit is Invalid");

            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            IHabitRepository repo = new PostgresHabitRepository(_connection, null);
            repo.Create(habit);

            return habit;
        }

        [HttpPut("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> UpdateHabit(Guid userID, Guid id, [FromBody] RequestData data)
        {
            string name = data.UpdateName;
            string[] off = data.UpdateDaysOff;

            Habit toUpdateHabit = Habit.addNewHabit(userID, name, off);
            if (toUpdateHabit == null) return NotFound("Invalid Habit format to update");

            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            IHabitRepository repo = new PostgresHabitRepository(_connection, null);

            Habit toUpdate = repo.FindByIdAndUserId(id, userID);
            if (toUpdate == null) return NotFound("Habit is not founded");

            repo.UpdateHabit(toUpdate, toUpdateHabit.Name, toUpdateHabit.DayOff);
            if (data.UpdateName.Equals(toUpdate.Name)) return NotFound("Habit is not updated");
            toUpdate = repo.FindByIdAndUserId(id, userID);

            return toUpdate;
        }

        [HttpDelete("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> DeleteHabit(Guid userID, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            IHabitRepository repo = new PostgresHabitRepository(_connection, null);
            Habit toDelete = repo.FindByIdAndUserId(id, userID);
            if (toDelete == null) return NotFound("Habit is not founded");

            repo.DeleteHabit(toDelete);

            Habit Deleted = repo.FindByIdAndUserId(id, userID);
            if (Deleted == null) return toDelete;

            return NotFound("Habit is not deleted");
        }

        [HttpPost("api/v1/users/{userID}/habits/{id}/logs")]
        public ActionResult<Habit> Log(Guid userID, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(_connstringgg);
            _connection.Open();

            ILogsRepository repoLog = new PostgresLogsRepository(_connection, null);
            IHabitRepository repo = new PostgresHabitRepository(_connection, null);

            Logs founded_log = repoLog.FindByHabitIdAndUserId(id, userID);
            if(founded_log == null) return NotFound("This habit has no log history");

            Habit founded_habit = repo.FindByIdAndUserId(id, userID);
            if(founded_habit == null) return NotFound("The habit is doesn't exist");

            Habit founded = new Habit(founded_habit.ID, founded_habit.UserId, founded_habit.Name, founded_habit.DayOff, new Logs(founded_log.LogCount, founded_log.GetLogDate));
            _connection.Close();
            return founded;
        }
    }
}
