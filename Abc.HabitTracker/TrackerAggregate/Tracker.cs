using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public class Tracker
    {
        private Guid _id;
        private Guid _habit_id;
        private Guid _user_id;
        private DateTime _date;

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public Tracker(Guid habit_id, Guid user_id, DateTime date)
        {
            this._id = Guid.NewGuid();
            this._habit_id = habit_id;
            this._user_id = user_id;
            this._date = date;
        }

        public Tracker(Guid id, Guid habit_id, Guid user_id, DateTime date)
        {
            this._id = id;
            this._habit_id = habit_id;
            this._user_id = user_id;
            this._date = date;
        }

        public static Tracker AddTracker(HabitTrack tracked)
        {
            return new Tracker(tracked.Todo.ID, tracked.Person, tracked.Date);
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}