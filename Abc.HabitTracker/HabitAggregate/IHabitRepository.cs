using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public interface IHabitRepository
    {
        Habit FindById(Guid id);
        void Create(Habit habit);
        Habit FindByIdAndUserId(Guid id, Guid user_id);
        void UpdateHabit(Habit habit, string name, string[] days);
        List<Habit> FindByUserId(Guid user_id);
        void DeleteHabit(Habit habit);
    }
}