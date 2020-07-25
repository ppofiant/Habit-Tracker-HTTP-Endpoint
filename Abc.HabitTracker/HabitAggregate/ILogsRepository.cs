using System;

namespace Abc.HabitTracker
{
    public interface ILogsRepository
    {
        Logs FindByHabitIdAndUserId(Guid habit_id, Guid user_id);
        void AddLogs(Track habitTrack);
    }
}