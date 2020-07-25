using System;
using System.Collections.Generic;

namespace Abc.HabitTracker
{
    public interface IBadgeRepository
    {
        Badge FindById(Guid id);
        void CreateBadge(Badge badge, DateTime date_created);
        List<Badge> FindByUserId(Guid user_id);
    }
}