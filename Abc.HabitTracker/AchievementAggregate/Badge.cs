using System;

namespace Abc.HabitTracker
{

    public class Badge
    {
        private Guid _id;
        private Guid _user_id;
        private string _name;
        private string _description;
        private DateTime _date;

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public Guid UserId
        {
            get
            {
                return _user_id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public Badge(Guid user_id, string name, string description, DateTime date = new DateTime()) {
            this._id = Guid.NewGuid();
            this._user_id = user_id;
            this._name = name;
            this._description = description;
            if(date == null)
            {
                this._date = DateTime.Now;
            }
            else
            {
                this._date = date;
            }
        }

        public Badge(Guid id, Guid user_id, string name, string description, DateTime date)
        {
            this._id = id;
            this._user_id = user_id;
            this._name = name;
            this._description = description;
            this._date = date;
        }

        public static Badge newBadge(Guid user_id, string name, string description) {
            return new Badge(user_id, name, description);
        }

        public override bool Equals(object obj)
        {
            var badge = obj as Badge;
            if(badge == null) return false;

            return this._id == badge._id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}