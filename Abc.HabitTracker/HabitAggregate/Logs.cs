using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public class Logs
    {
        protected int _logscount;
        private int _streak;
        private int _longeststreak;
        private List<DateTime> _date = new List<DateTime>();

        public int LogCount
        {
            get
            {
                return _logscount;
            }
        }

        public int Streak
        {
            get
            {
                return _streak;
            }
        }

        public int LongestStreak
        {
            get
            {
                return _longeststreak;
            }
        }

        public List<DateTime> GetLogDate
        {
            get
            {
                return _date;
            }
        }

        public Logs()
        {
            this._logscount = 0;
            this._streak = 0;
            this._longeststreak = 0;
            this._date = new List<DateTime>();
        }

        public Logs(int count, List<DateTime> date)
        {
            int streaks = 0;
            if (date.Count != 1)
            {
                for (int i = 0; i < date.Count-1; i++)
                {
                    DateTime nextDay = date[i].AddDays(1);
                    if(nextDay.Equals(date[i+1]))
                    {
                        streaks++;
                    }
                }
            }
            if(_longeststreak < streaks)
            {
                this._longeststreak = streaks+1;
                this._streak = streaks+1;
            }
            if(date.Count == 0)
            {
                this._streak = 0;
            }
            else
            {
                this._streak = streaks + 1;
            }
            this._logscount = count;
            this._date = date;
        }

        public Logs(int count, DateTime date)
        {
            if (count < 0)
            {
                throw new Exception("score can't be negative");
            }
            this._logscount = count;
            this._date.Add(date);
        }

        public Logs(int count, int streak)
        {
            if (count < 0 || streak < 0)
            {
                throw new Exception("count or streak can't be negative");
            }

            this._logscount = count;
            this._streak = streak;

            if (this._streak < this._longeststreak)
            {
                this._longeststreak = streak;
            }
        }

        public Logs Add(int score, DateTime date)
        {
            if (score < 0)
            {
                throw new Exception("score can't be negative");
            }
            return new Logs(this._logscount + score, date);
        }
    }
}