﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public enum EngineerExpireance //Expireance Level Of The Engineers
{
    Beginner = 0,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert
}
public enum TaskStatus
{
    Unscheduled,
    Scheduled,
    OnTrack,
    Done
}
