using System;
using System.Collections.Generic;

using Abc.HabitTracker.AchievementGainer;

namespace Abc.HabitTracker
{
    public class DailyFactory
    {
        public static AbcApplication Create(List<Habit> listHabit)
        {
            if(listHabit == null)
            {
                throw new Exception("the Habit is not created yet");
            }

            AbcApplication daily = new HabitTracker(listHabit);
            Habit habits = null;
            DominatingHandler dominating = new DominatingHandler(new DominatingGain());
            WorkaholicHandler workaholic = new WorkaholicHandler(new WorkaholicGain());
            EpicComebackHandler epiccomeback = new EpicComebackHandler(new EpicComebackGain());

            habits.Attach(dominating);
            habits.Attach(workaholic);
            habits.Attach(epiccomeback);

            return daily;
        }
    }
}