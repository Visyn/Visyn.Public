#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Text;
using Visyn.Types.Time;

namespace Visyn.Automation
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
                    TimeRemaining = TimeSpan.Zero;
                    break;
                default:    // Estimate end time
                    EndTime = StartTime + TimeSpan.FromTicks((long)((100.0 / Progress) * TimeElapsed.Ticks));
                    TimeRemaining = EndTime - now;
                    break;
            }
        }

        #region Overrides of ValueType

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder("");
            if (TasksTotal > 0) builder.Append($"{TasksComplete}/{TasksTotal}");
            if (TimeRemaining > TimeSpan.Zero) builder.Append($" {TimeRemaining.Round(TimeSpan.FromSeconds(1)):g}");
            else if (Progress == 100) builder.Append($" {TimeElapsed.Round(TimeSpan.FromSeconds(1)):g}");
            return builder.ToString();
        }

        #endregion
    }
}