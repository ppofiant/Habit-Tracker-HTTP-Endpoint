using System;

namespace Abc.HabitTracker
{
    public class WeekDays
    {
        private string[] weekDays = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        private string[] offDays;

        public string[] OffDays
        {
            get
            {
                return offDays;
            }
        }

        public WeekDays(string[] off = null)
        {
            if (Check(off) == true)
            {
                this.offDays = off;
            }
        }

        public bool Check(string[] off = null)
        {
            if (OverLength(off) == true)
            {
                return false;
            }
            if (isValid(off) == false)
            {
                return false;
            }
            if (isDupplicate(off) == true)
            {
                return false;
            }
            return true;
        }

        private bool isValid(string[] off)
        {
            for (int i = 0; i < off.Length; i++)
            {
                bool valid = false;
                for (int j = 0; j < weekDays.Length; j++)
                {
                    if (off[i] == weekDays[j]) valid = true;
                }
                if (valid == false) return false;
            }
            return true;
        }

        private bool isDupplicate(string[] off)
        {
            if(off.Length == 1) return false;

            for (int i = 0; i < off.Length; i++)
            {
                for (int j = 0; j < off.Length ; j++)
                {
                    if (off[j].Equals(off[i]) && j != i) return true;
                }
            }
            return false;
        }

        private bool OverLength(string[] off)
        {
            if (off.Length >= 7)
            {
                return true;
            }
            return false;
        }
    }
}