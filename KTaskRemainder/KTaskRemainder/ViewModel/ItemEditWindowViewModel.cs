using System;
using System.Windows.Input;
using KTaskRemainder.Common;
using KTaskRemainder.Model;
using KTaskRemainder.View;

namespace KTaskRemainder.ViewModel
{
    public class ItemEditWindowViewModel : ViewModelBase
    {
        private TaskWidget _taskWidget;
        private ItemEditWindow _window;

        private ICommand _closeCommandOk;
        private ICommand _closeCommandConcel;

        public ICommand CloseCommandOk
        {
            get
            {
                if (_closeCommandOk == null)
                {
                    _closeCommandOk = new CommandBase((o) => { _window.DialogResult = true; _window.Close(); }, null);
                }
                return _closeCommandOk;
            }
        }

        public ICommand CloseCommandCancel
        {
            get
            {
                if (_closeCommandConcel == null)
                {
                    _closeCommandConcel = new CommandBase((o) => { _window.DialogResult = false; _window.Close(); }, null);
                }
                return _closeCommandConcel;
            }
        }

        public ItemEditWindowViewModel(ItemEditWindow window, TaskWidget taskWidget)
        {
            _window = window;
            _taskWidget = taskWidget;
        }

        public string ItemContent
        {
            get
            {
                return _taskWidget.TaskContent;
            }
            set
            {
                _taskWidget.TaskContent = value;
                this.OnNotifyPropertyChanged("ItemContent");
            }
        }

        public void SetTaskWidget(TaskWidget taskWidget)
        {
            _taskWidget = taskWidget;
        }
    }
}
