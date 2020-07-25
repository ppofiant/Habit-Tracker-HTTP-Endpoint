using System;
using Xunit;
using System.Collections.Generic;
using Abc.HabitTracker.Database.Postgres;
using Abc.HabitTracker.AchievementGainer;
using Npgsql;

namespace Abc.HabitTracker.Test
{
    public class TrackerRepoTest
    {
        private string _connstring;

        public TrackerRepoTest()
        {
            _connstring = "Host=localhost;Username=postgres;Password=postgres;Database=abc;Port=5432";
        }

        [Fact]
        public void CheckAchievementDominating()
        {
            IAchievementGainer _dominate = new DominatingGain();

            NpgsqlConnection connection = new NpgsqlConnection(_connstring);
            connection.Open();

            User popo = User.NewUser("Popo");

            IHabitRepository habitRepo = new PostgresHabitRepository(connection, null);
            IBadgeRepository badgeRepo = new PostgresBadgeRepository(connection, null);

            Habit belajar = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Sat", "Mon" });
            habitRepo.Create(belajar);
            Habit olahraga = Habit.addNewHabit(popo.ID, "Olahraga", new string[] { "Mon" });
            habitRepo.Create(olahraga);
            List<Habit> habitList = new List<Habit>();

            habitList.Add(belajar);
            habitList.Add(olahraga);
            AbcApplication daily = new HabitTracker(habitList);

            ILogsRepository repoLogs = new PostgresLogsRepository(connection, null);

            Track track;
            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 1));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 2));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 3));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 4));
            daily.Do(track);
            repoLogs.AddLogs(track);

            Habit founded_habit = habitRepo.FindByIdAndUserId(belajar.ID, popo.ID);
            int listSize = founded_habit.Log.GetLogDate.Count;
            Assert.True(founded_habit.isDominating() == true);

            if (founded_habit.isDominating())
            {
                badgeRepo.CreateBadge(_dominate.GainAchievement(popo.ID), founded_habit.Log.GetLogDate[listSize - 1]);
            }

            connection.Close();
        }

        [Fact]
        public void checkWorkaholic()
        {
            IAchievementGainer _workaholic = new WorkaholicGain();

            NpgsqlConnection connection = new NpgsqlConnection(_connstring);
            connection.Open();

            User popo = User.NewUser("Popo");

            IHabitRepository habitRepo = new PostgresHabitRepository(connection, null);
            IBadgeRepository badgeRepo = new PostgresBadgeRepository(connection, null);

            Habit belajar = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Sat", "Mon" });
            habitRepo.Create(belajar);
            Habit olahraga = Habit.addNewHabit(popo.ID, "Olahraga", new string[] { "Mon" });
            habitRepo.Create(olahraga);
            List<Habit> habitList = new List<Habit>();

            habitList.Add(belajar);
            habitList.Add(olahraga);
            AbcApplication daily = new HabitTracker(habitList);

            ILogsRepository repoLogs = new PostgresLogsRepository(connection, null);

            Track track;
            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 7));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 9));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 14));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 16));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 21));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 23));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 28));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 30));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 4, 4));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 4, 6));
            daily.Do(track);
            repoLogs.AddLogs(track);

            Habit founded_habit = habitRepo.FindByIdAndUserId(belajar.ID, popo.ID);
            List<DateTime> h = founded_habit.Log.GetLogDate;
            int listSize = founded_habit.Log.GetLogDate.Count;

            if (founded_habit.isWorkaholic())
            {
                badgeRepo.CreateBadge(_workaholic.GainAchievement(popo.ID), founded_habit.Log.GetLogDate[listSize - 1]);
            }

            Assert.True(founded_habit.isWorkaholic() == true);
        }

        [Fact]
        public void checkEpicComeback()
        {
            IAchievementGainer _epiccomeback = new EpicComebackGain();

            NpgsqlConnection connection = new NpgsqlConnection(_connstring);
            connection.Open();

            User popo = User.NewUser("Popo");

            IHabitRepository habitRepo = new PostgresHabitRepository(connection, null);
            IBadgeRepository badgeRepo = new PostgresBadgeRepository(connection, null);

            Habit belajar = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Sat", "Mon" });
            habitRepo.Create(belajar);
            Habit olahraga = Habit.addNewHabit(popo.ID, "Olahraga", new string[] { "Mon" });
            habitRepo.Create(olahraga);
            List<Habit> habitList = new List<Habit>();

            habitList.Add(belajar);
            habitList.Add(olahraga);
            AbcApplication daily = new HabitTracker(habitList);

            ILogsRepository repoLogs = new PostgresLogsRepository(connection, null);

            Track track;
            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 7));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 18));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 19));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 20));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 21));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 22));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 23));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 24));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 25));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 26));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 27));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 28));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 29));
            daily.Do(track);
            repoLogs.AddLogs(track);

            track = new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 30));
            daily.Do(track);
            repoLogs.AddLogs(track);

            Habit founded_habit = habitRepo.FindByIdAndUserId(belajar.ID, popo.ID);
            List<DateTime> h = founded_habit.Log.GetLogDate;
            int listSize = founded_habit.Log.GetLogDate.Count;

            if (founded_habit.isEpicComeback())
            {
                badgeRepo.CreateBadge(_epiccomeback.GainAchievement(popo.ID), founded_habit.Log.GetLogDate[listSize - 1]);
            }

            Assert.True(founded_habit.isEpicComeback() == true);
        }
    }
}