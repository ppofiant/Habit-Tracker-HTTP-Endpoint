using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public class User
    {
        private Guid _id;
        private string _name;
        private List<Badge> _achievement;

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public List<Badge> Achievements
        {
            get
            {
                return _achievement;
            }
        }

        public User(Guid id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public User(Guid id, string name, List<Badge> badge)
        {
            this._id = id;
            this._name = name;
            this._achievement = badge;
        }

        public static User NewUser(string name)
        {
            return new User(Guid.NewGuid(), name);
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            if (user == null) return false;

            return this._id == user._id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
