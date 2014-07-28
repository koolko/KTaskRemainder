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
using System.Windows.Input;
using KTaskRemainder.Database;

namespace KTaskRemainder.ViewModel
{
    /// <summary>
    /// ViewModel class for 'TaskWidgets'
    /// </summary>
    public class TaskWidgetsViewModel : ViewModelBase
    {
        /// <summary>
        /// Collection of the 'TaskWidget' items
        /// </summary>
        private ObservableCollection<TaskWidget> _taskWidgets;

        /// <summary>
        /// Current item
        /// </summary>
        private TaskWidget _currentTask;

        /// <summary>
        /// 'DropHandler' class
        /// </summary>
        private ListBoxDropHandler _listBoxDropHandler;

        /// <summary>
        /// 'AddItemCommand' command
        /// </summary>
        private ICommand _addItemCommand;

        /// <summary>
        /// 'RemoveItemCommand' command
        /// </summary>
        private ICommand _removeItemCommand;

        /// <summary>
        /// Gets the 'AddItemCommand' command
        /// </summary>
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new CommandBase((o) =>
                    {
                        if (o is System.Windows.Controls.UserControl)
                        {
                            System.Windows.Controls.UserControl uc = (System.Windows.Controls.UserControl)o;
                            bool important = ((string)uc.Tag == "1" || (string)uc.Tag == "3");
                            bool urgent = ((string)uc.Tag == "1" || (string)uc.Tag == "2");
                            TaskWidget tw = new TaskWidget("<New Item>", important, urgent);
                            _taskWidgets.Add(tw);
                            DbManager.Add(tw.TaskGuid, tw.TaskContent, tw.Important, tw.Urgent);
                        }
                    });
                }
                return _addItemCommand;
            }
        }

        /// <summary>
        /// Gets the 'RemoveItemCommand' command
        /// </summary>
        public ICommand RemoveItemCommand
        {
            get
            {
                if (_removeItemCommand == null)
                {
                    _removeItemCommand = new CommandBase((o) => _taskWidgetRemove());
                }
                return _removeItemCommand;
            }
        }

        /// <summary>
        /// 'TaskWidgetsViewModel' constructor
        /// </summary>
        /// <param name="collectionName">Name of the TaskWidget collection</param>
        public TaskWidgetsViewModel(string collectionName)
        {
            _taskWidgets = TaskWidgetManager.Instance.CreateTaskWidgetCollection(collectionName);

            List<DbCommon.Task> tasks = null;
            bool important = (collectionName.ToLower().Contains("first") || collectionName.ToLower().Contains("third"));
            bool urgent = (collectionName.ToLower().Contains("first") || collectionName.ToLower().Contains("second"));
            if (DbManager.GetAllTasks(out tasks, important, urgent))
            {
                foreach (DbCommon.Task task in tasks)
                {
                    _taskWidgets.Add(new TaskWidget(task.Text, task.Important, task.Urgent, task.Guid));
                }
            }
        }

        /// <summary>
        /// Gets or sets the TaskWidget collection
        /// </summary>
        public ObservableCollection<TaskWidget> TaskWidgets
        {
            get { return _taskWidgets; }
            private set
            {
                _taskWidgets = value;
            }
        }

        /// <summary>
        /// Gets or sets the current task widget
        /// </summary>
        public TaskWidget CurrentTask
        {
            get { return _currentTask; }
            set
            {
                _currentTask = value;
                this.OnNotifyPropertyChanged("CurrentTask");
            }
        }

        /// <summary>
        /// Gets the ListBox DropHandler
        /// </summary>
        public ListBoxDropHandler ListBoxDropHandler
        {
            get
            {
                if (_listBoxDropHandler == null)
                {
                    _listBoxDropHandler = new ListBoxDropHandler();
                }
                return _listBoxDropHandler;
            }
        }

        /// <summary>
        /// Executes at the time of the removel of controls
        /// </summary>
        private void _taskWidgetRemove()
        {
            DbManager.Remove(_currentTask.TaskGuid);
            _taskWidgets.Remove(_currentTask);
        }
    }
}
