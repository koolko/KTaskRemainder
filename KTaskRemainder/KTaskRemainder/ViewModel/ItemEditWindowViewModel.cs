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

        private ICommand _closeCommandOk;
        private ICommand _closeCommandCancel;

        public ICommand CloseCommandOk
        {
            get
            {
                if (_closeCommandOk == null)
                {
                    _closeCommandOk = new CommandBase((o) => { if (o is System.Windows.Window) { ((System.Windows.Window)o).DialogResult = true; ((System.Windows.Window)o).Close(); } }, null);
                }
                return _closeCommandOk;
            }
        }

        public ICommand CloseCommandCancel
        {
            get
            {
                if (_closeCommandCancel == null)
                {
                    _closeCommandCancel = new CommandBase((o) => { if (o is System.Windows.Window) { ((System.Windows.Window)o).DialogResult = false; ((System.Windows.Window)o).Close(); } }, null);
                }
                return _closeCommandCancel;
            }
        }

        public ItemEditWindowViewModel(TaskWidget taskWidget)
        {
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
