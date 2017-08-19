using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Visyn.Threads
{
    public static class DispatcherExtensions
    {
        [Conditional("DEBUG")]
        public static void AssertAccess(this Dispatcher dispatcher)
        {
            Debug.Assert(dispatcher.CheckAccess());
        }
    }
}
