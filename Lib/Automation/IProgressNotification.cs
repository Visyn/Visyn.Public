using System.ComponentModel;

namespace Visyn.Public.Automation
{
    public interface IProgressNotification : INotifyPropertyChanged
    {
        TaskInfo TaskInfo { get; }

        void ClearStatistics();
        void Start(int tasksToRun);
    }
}