using System;

namespace Abc.HabitTracker.AchievementGainer
{
    public interface IAchievementGainer
    {
        Badge GainAchievement(Guid user_id);
    }
}