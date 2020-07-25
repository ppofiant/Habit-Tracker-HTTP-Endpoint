using System;

namespace Abc.HabitTracker
{
    public abstract class TrackResult
    {
        public Habit Habits { private set;  get; }
        
        public Guid HabitsUserId
        {
            get
            {
                return Habits.UserId;
            }
        }

        public TrackResult(Habit habit)
        {
            this.Habits = habit;
        }
    }

    public class Dominating : TrackResult
    {
        public Dominating(Habit habit) : base(habit) { }
    }

    public class Workaholic : TrackResult
    {
        public Workaholic(Habit habit) : base(habit) { }
    }

    public class EpicComeback : TrackResult
    {
        public EpicComeback(Habit habit) : base(habit) { }
    }
}