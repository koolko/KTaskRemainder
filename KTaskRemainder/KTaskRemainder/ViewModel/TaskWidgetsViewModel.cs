using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using KTaskRemainder.Model;
using KTaskRemainder.Common;
using KTaskRemainder.Behavior;
using System.Windows;

namespace KTaskRemainder.ViewModel
{
    public class TaskWidgetsViewModel : ViewModelBase
    {
        private ObservableCollection<TaskWidget> _taskWidgets;
        private TaskWidget _currentTask;
        private string _collectionName;

        private CommandBase _startDragAction;

        public string CollectionName
        {
            get { return _collectionName; }
        }

        public CommandBase StartDragAction
        {
            get { return _startDragAction; }
        }

        public TaskWidgetsViewModel(string collectionName)
        {
            _collectionName = collectionName;
            _taskWidgets = TaskWidgetManager.Instance.CreateTaskWidgetCollection(collectionName);
        }

        public ObservableCollection<TaskWidget> TaskWidgets
        {
            get { return _taskWidgets; }
            private set
            {
                _taskWidgets = value;
            }
        }

        public TaskWidget CurrentTask
        {
            get { return _currentTask; }
            set
            {
                _currentTask = value;
                this.OnNotifyPropertyChanged("CurrentTask");
            }
        }
    }
}
