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
using NpgsqlTypes;

namespace Abc.HabitTracker.Api.Controllers
{
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private string _constringg;

        public BadgesController(ILogger<BadgesController> logger)
        {
            _logger = logger;
            _constringg = "Host=localhost;Username=postgres;Password=postgres;Database=abc;Port=5432";
        }

        [HttpGet("api/v1/users/{userID}/badges")]
        public ActionResult<IEnumerable<Badge>> All(Guid userID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(_constringg);
            connection.Open();

            IBadgeRepository badgeRepo = new PostgresBadgeRepository(connection, null);

            List<Badge> founded_badge = badgeRepo.FindByUserId(userID);
            if(founded_badge == null) return NotFound("No Badge in this user");

            return founded_badge;
        }
    }
}
