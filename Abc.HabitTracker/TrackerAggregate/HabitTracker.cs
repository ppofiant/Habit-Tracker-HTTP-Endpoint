using System;
using System.Collections.Generic;

using Abc.HabitTracker.LogGainer;

namespace Abc.HabitTracker
{
    public class HabitTracker : AbcApplication
    {
        public HabitTracker(List<Habit> habits) : base(habits) { }

        private int _size;
        public int Size
        {
            get
            {
                return _size;
            }
        }

        public override void Init()
        {
            if (this.Habits.Count == 0)
            {
                throw new Exception("Habit are not set");
            }
            _size = Habits.Count;
        }

        public override void Do(Track habitTrack)
        {
            HabitTrack ht = habitTrack as HabitTrack;
            if (ht == null)
            {
                throw new Exception("habitTrack is not valid");
            }

            bool IsFounded = false;
            foreach (Habit list in Habits)
            {
                if (ht.Todo == list)
                {
                    IsFounded = true;
                    ht.Todo.AddLogsHabit(_gainer.Gain(), ht.Date);
                    break;
                }
            }
            if (IsFounded == false)
            {
                throw new Exception("Not habit is founded");
            }
        }
    }

    public class HabitTrack : Track
    {
        private DateTime _date;

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public int DateIndex
        {
            get
            {
                return Convert.ToInt32(Date.ToString("dd"));
            }
        }

        public string DayDate
        {
            get
            {
                return _date.ToString("ddd");
            }
        }

        public HabitTrack(Habit habit, Guid usr_id, DateTime date) : base(habit, usr_id)
        {
            this._date = date;
        }
    }
}