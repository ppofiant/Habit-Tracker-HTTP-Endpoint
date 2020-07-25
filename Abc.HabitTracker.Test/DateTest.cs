using System;
using Xunit;
using System.Collections.Generic;

namespace Abc.HabitTracker.Test
{
    public class DateTest
    {
        [Fact]
        public void CheckDay()
        {
            DateTime dt = DateTime.Now;
            string day = dt.DayOfWeek.ToString(); // untuk dapat hari nya

            Assert.Equal("Sunday", day);
        }

        [Fact]
        public void HabitTrackTest()
        {
            User popo = User.NewUser("Popo");
            List<Habit> list = new List<Habit>();

            Habit habit = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Tue", "Wed" });
            Habit habit2 = Habit.addNewHabit(popo.ID, "Mandi", new string[] { "Mon" });

            list.Add(habit);
            list.Add(habit2);

            string name = null;
            foreach (var i in list)
            {
                if (i.Name == habit.Name)
                {
                    name = i.Name;
                    break;
                }
            }

            Assert.NotNull(list);
            Assert.Equal(name, habit.Name);
        }

        [Fact]
        public void testtesttest()
        {
            User popo = User.NewUser("Popo");

            List<Habit> habits = new List<Habit>();
            Habit belajar = Habit.addNewHabit(popo.ID, "Belajar", new string[] { "Sat", "Sun" });

            habits.Add(belajar);
            AbcApplication daily = new HabitTracker(habits);
        }
    }
}