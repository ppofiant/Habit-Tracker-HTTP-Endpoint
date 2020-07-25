using System;
using System.Text.RegularExpressions;
using Abc.HabitTracker.Database.Postgres;
using Abc.HabitTracker;
using Npgsql;
using System.Collections.Generic;

namespace template_uas_psd_habit_tracker
{
    public class Program
    {
        static void Main(string[] args)
        {
            DateTime[] dateArray = new DateTime[] { new DateTime(2020, 03, 01), new DateTime(2020, 03, 02) };
            foreach (var i in dateArray)
            {
                Console.WriteLine(i);
            }

            List<DateTime> date = new List<DateTime>();

            date.Add(DateTime.Now);
            date.Add(new DateTime(2020, 05, 02));

            foreach (var i in date)
            {
                Console.WriteLine(i);
            }
        }
    }
}
