﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE.Managers;

namespace ACE.Entity.Actions
{
    /// <summary>
    /// Action that will not return until WorldManager.PortalTickYears >= EndTime
    /// must only be inserted into DelayManager actor
    /// </summary>
    public class DelayAction : ActionEventBase, IComparable<DelayAction>
    {
        public double WaitTime { get; private set; }
        public double EndTime { get; private set; }

        // For breaking ties on compareto, two actions cannot be equal
        private long sequence;
        private volatile static uint glblSequence = 0;

        public DelayAction(double waitTimePortalTickYears) : base()
        {
            WaitTime = waitTimePortalTickYears;
            sequence = glblSequence++;
        }

        public void Start()
        {
            EndTime = WorldManager.PortalYearTicks + WaitTime;
        }

        public int CompareTo(DelayAction rhs)
        {
            int ret = EndTime.CompareTo(rhs.EndTime);
            if (ret == 0)
            {
                return sequence.CompareTo(rhs.sequence);
            }
            return ret;
        }
    }
}
