using System;

using Abc.HabitTracker.AchievementGainer;

namespace Abc.HabitTracker
{
    public abstract class TrackResultHandler : IObserver<TrackResult>
    {
        protected IAchievementGainer _gainer;

        public TrackResultHandler(IAchievementGainer gainer)
        {
            this._gainer = gainer;
        }

        public abstract void Update(TrackResult e);
    }

    public class DominatingHandler : TrackResultHandler
    {
        public DominatingHandler(IAchievementGainer gainer) : base(gainer) { }

        public override void Update(TrackResult e)
        {
            Dominating ev = e as Dominating;
            if(ev == null) return;

            ev.Habits.AddLogsHabit(1, DateTime.Now);
        }
    }

    public class WorkaholicHandler : TrackResultHandler
    {
        public WorkaholicHandler(IAchievementGainer gainer) : base(gainer) { }

        public override void Update(TrackResult e)
        {
            Workaholic ev = e as Workaholic;
            if(ev == null) return;

            ev.Habits.AddLogsHabit(1, DateTime.Now);
        }
    }

    public class EpicComebackHandler : TrackResultHandler
    {
        public EpicComebackHandler(IAchievementGainer gainer) : base(gainer) { }

        public override void Update(TrackResult e)
        {
            EpicComeback ev = e as EpicComeback;
            if(ev == null) return;

            ev.Habits.AddLogsHabit(1, DateTime.Now);
        }
    }

}