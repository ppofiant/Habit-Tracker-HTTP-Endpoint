using System;

using Abc.HabitTracker.AchievementGainer;

namespace Abc.HabitTracker
{
    public class DominatingGain : IAchievementGainer
    {
        public Badge GainAchievement(Guid user_id)
        {
            return Badge.newBadge(user_id, "Dominating", "4+ streak");
        }
    }

    public class WorkaholicGain : IAchievementGainer
    {
        public Badge GainAchievement(Guid user_id)
        {
            return Badge.newBadge(user_id, "Workaholic", "Doing some works on days-off");
        }
    }

    public class EpicComebackGain : IAchievementGainer
    {
        public Badge GainAchievement(Guid user_id)
        {
            return Badge.newBadge(user_id, "Epic Comeback", "10 steak after 10 days without logging");
        }
    }
}