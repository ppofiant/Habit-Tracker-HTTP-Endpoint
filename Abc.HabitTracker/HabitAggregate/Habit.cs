using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public class Habit : IObservable<TrackResult>
    {
        private Guid _id;
        private Guid _user_id;
        protected string _name;
        protected WeekDays _off;
        protected Logs _log;

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            set
            {
                this._name = value;
            }
            get
            {
                return _name;
            }
        }

        public string[] DayOff
        {
            get
            {
                return _off.OffDays;
            }
        }

        public Logs Log
        {
            get
            {
                return _log;
            }
        }

        public Guid UserId
        {
            get
            {
                return _user_id;
            }
        }

        public Habit(Guid user_id, string name, string[] off)
        {
            this._id = Guid.NewGuid();
            this._user_id = user_id;
            this._name = name;
            this._off = new WeekDays(off);
            this._log = new Logs();
        }

        public Habit(Guid id, Guid user_id, string name, string[] off, Logs log)
        {
            this._id = id;
            this._user_id = user_id;
            this._name = name;
            this._off = new WeekDays(off);
            this._log = log;
        }

        protected List<IObserver<TrackResult>> _observers = new List<IObserver<TrackResult>>();
        public void Attach(IObserver<TrackResult> obs)
        {
            _observers.Add(obs);
        }

        public void Broadcast(TrackResult e)
        {
            foreach (var obs in _observers)
            {
                obs.Update(e);
            }
        }

        public static Habit addNewHabit(Guid user_id, string name, string[] off)
        {
            return new Habit(user_id, name, off);
        }

        public void AddLogsHabit(int size, DateTime date)
        {
            List<DateTime> dateBefore = _log.GetLogDate;
            foreach (DateTime i in dateBefore)
            {
                if (date.Equals(i)) throw new Exception("this habit for that day has been tracked");
            }
            this._log = this._log.Add(size, date);

            if(isDominating())
            {
                Broadcast(new Dominating(this));
            }
            if(isWorkaholic())
            {
                Broadcast(new Workaholic(this));
            }
            if(isEpicComeback())
            {
                Broadcast(new EpicComeback(this));
            }
        }

        public bool isDominating()
        {
            int dominate = 0;
            List<DateTime> listDate = Log.GetLogDate;
            for (int i = 0; i < listDate.Count - 1; i++)
            {
                DateTime nextDay = listDate[i].AddDays(1);
                if (nextDay.Equals(listDate[i + 1]))
                {
                    dominate++;
                }
                else
                {
                    bool inOffDay = false;
                    string off = nextDay.ToString("ddd");
                    foreach (string j in DayOff)
                    {
                        if (j.Equals(off))
                        {
                            inOffDay = true;
                            break;
                        }
                    }
                    if (inOffDay == false)
                    {
                        dominate = 0;
                    }
                }
                if (dominate + 1 >= 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isWorkaholic()
        {
            int workaholic = 0;
            List<DateTime> listDate = Log.GetLogDate;
            for (int i = 0; i < listDate.Count; i++)
            {
                string currentDate = listDate[i].ToString("ddd");
                foreach (var j in DayOff)
                {
                    if (j.Equals(currentDate))
                    {
                        workaholic++;
                    }
                }
                if (workaholic >= 10)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isEpicComeback()
        {
            bool after10daynotloggin = false;
            int epiccomeback = 0;
            List<DateTime> listDate = Log.GetLogDate;
            for (int i = 0; i < listDate.Count - 1; i++)
            {
                if (after10daynotloggin == false)
                {
                    DateTime day_10 = listDate[i].AddDays(10);
                    if (DateTime.Compare(day_10, listDate[i + 1]) == -1)
                    {
                        after10daynotloggin = true;
                    }
                }
                else
                {
                    DateTime nextDay = listDate[i].AddDays(1);
                    if (nextDay == listDate[i + 1])
                    {
                        epiccomeback++;
                    }
                    else
                    {
                        bool inOffDay = false;
                        string off = nextDay.ToString("ddd");
                        foreach (string j in DayOff)
                        {
                            if (j.Equals(off))
                            {
                                inOffDay = true;
                            }
                        }
                        if (inOffDay == false)
                        {
                            epiccomeback = 0;
                        }
                    }
                }
                if (epiccomeback >= 10)
                {
                    return true;
                }
            }
            return false;
        }
    }
}