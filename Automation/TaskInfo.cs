using System;
using System.Diagnostics;

namespace Visyn.Public.Automation
{
    public struct TaskInfo
    {
        public int TasksTotal { get; }
        public int TasksRemaining { get; }
        public int TasksComplete { get; }
        public int Progress { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan TimeElapsed { get; }
        public TimeSpan TimeRemaining { get; }
        //        public TimeSpan Duration { get; }

        //public TaskInfo()
        //{
        //    TasksTotal = 0;
        //    TasksRemaining = 0;
        //    TasksComplete = 0;
        //    Progress = 0;
        //    StartTime = DateTime.MinValue;
        //    EndTime = DateTime.MinValue;
        //    TimeElapsed = TimeSpan.Zero;
        //    TimeRemaining = TimeSpan.Zero;
        //}

        private TaskInfo(uint tasksComplete, uint tasksTotal) : this()
        {
            // Note: default(DateTime) = DateTime.MinValue & default(TimeSpan) == TimeSpan.Zero
            TasksComplete = Convert.ToInt32(tasksComplete);
            Debug.Assert(tasksComplete <= tasksTotal);
            TasksTotal = Convert.ToInt32(Math.Max(tasksTotal, tasksComplete));
            TasksRemaining = TasksTotal - TasksComplete;
            Progress = TasksTotal > 0 ? (100 * TasksComplete) / TasksTotal : 0;
            
        }

        public TaskInfo(uint tasksComplete, uint tasksTotal, DateTime startTime) : this(tasksComplete, tasksTotal)
        {
            var now = DateTime.Now;
            // Initialize start time if start time is not specified and tasks total > 0
            StartTime = startTime > DateTime.MinValue ? startTime : now;

            TimeElapsed = now - StartTime;
            switch (Progress)
            {
                case 100:
                    EndTime = now;
                    TimeRemaining = TimeSpan.Zero;
                    break;
                case 0:     // Cannot estimate end time...
                    EndTime = DateTime.MinValue;
                    TimeRemaining = TimeSpan.MaxValue;
                    break;
                default:    // Estimate end time
                    EndTime = StartTime + TimeSpan.FromTicks((long)((100.0 / Progress) * TimeElapsed.Ticks));
                    TimeRemaining = EndTime - now;
                    break;
            }
        }
    }
}