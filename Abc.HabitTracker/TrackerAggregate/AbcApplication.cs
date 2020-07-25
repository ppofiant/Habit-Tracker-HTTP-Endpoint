using System;
using System.Collections.Generic;

using Abc.HabitTracker.LogGainer;

namespace Abc.HabitTracker
{
    public abstract class AbcApplication
    {
        private List<Habit> _habits;
        protected ILogsGainer _gainer;

        public List<Habit> Habits
        {
            get
            {
                return _habits;
            }
        }

        public AbcApplication(List<Habit> habits)
        {
            this._habits = habits;
            this.Init();
        }

        public abstract void Init();
        public abstract void Do(Track habitTrack);
    }

    public abstract class Track
    {
        private Habit _todo;
        private Guid _userId;

        public Habit Todo
        {
            get
            {
                return _todo;
            }
        }

        public Guid Person
        {
            get
            {
                return _userId;
            }
        }

        public Track(Habit habit, Guid usr_id)
        {
            if (habit == null)
            {
                throw new Exception("Habit is null");
            }
            if (usr_id == null)
            {
                throw new Exception("User is null");
            }
            if (!habit.UserId.Equals(usr_id))
            {
                throw new Exception("User don't have this habit");
            }
            this._todo = habit;
            this._userId = usr_id;
        }
    }
}