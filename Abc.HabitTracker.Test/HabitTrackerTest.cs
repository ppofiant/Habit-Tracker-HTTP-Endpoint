using System;
using Xunit;
using System.Collections.Generic;

namespace Abc.HabitTracker.Test
{
    public class HabitTrackerTest
    {
        [Fact]
        public void TrackerTest()
        {
            User popo = User.NewUser("Popo");

            List<Habit> habits = new List<Habit>();
            Habit belajar = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Sat", "Sun" });
            Habit olahraga = Habit.addNewHabit(popo.ID, "Olahraga", new string[] { "Mon" });
            Habit main_game = Habit.addNewHabit(popo.ID, "Main Game", new string[] { "Mon" });

            habits.Add(belajar);
            habits.Add(olahraga);
            habits.Add(main_game);

            AbcApplication daily = new HabitTracker(habits);
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 1))); 
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 2)));
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 3)));
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 4)));
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 5)));
            daily.Do(new HabitTrack(belajar, popo.ID, new DateTime(2020, 3, 6)));

        }
    }
}