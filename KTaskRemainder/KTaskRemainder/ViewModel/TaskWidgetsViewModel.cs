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

namespace KTaskRemainder.ViewModel
{
    public class TaskWidgetsViewModel : ViewModelBase
    {
        private ObservableCollection<TaskWidget> _taskWidgets;
        private TaskWidget _currentTask;
        private ListBoxDropHandler _listBoxDropHandler;

        private ICommand _addItemCommand;

        private ICommand _removeItemCommand;

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
                            bool urgent = ((string)uc.Tag == "2" || (string)uc.Tag == "4");
                            _taskWidgets.Add(new TaskWidget("<New Item>", important, urgent));
                        }
                    });
                }
                return _addItemCommand;
            }
        }

        public ICommand RemoveItemCommand
        {
            get
            {
                if (_removeItemCommand == null)
                {
                    _removeItemCommand = new CommandBase((o) => _taskWidgets.Remove(_currentTask));
                }
                return _removeItemCommand;
            }
        }

        public TaskWidgetsViewModel(string collectionName)
        {
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
    }
}
