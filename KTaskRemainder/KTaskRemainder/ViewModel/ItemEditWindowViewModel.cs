using System;
using System.Windows.Input;
using KTaskRemainder.Common;
using KTaskRemainder.Model;
using KTaskRemainder.View;

namespace KTaskRemainder.ViewModel
{
    /// <summary>
    /// ViewModel class for 'ItemEditWindow'
    /// </summary>
    public class ItemEditWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Task widget to edit
        /// </summary>
        private TaskWidget _taskWidget;

        /// <summary>
        /// Content of the item
        /// </summary>
        private string _itemContent;

        /// <summary>
        /// 'CloseCommandOk' command
        /// </summary>
        private ICommand _closeCommandOk;

        /// <summary>
        /// 'CloseCommandCancel' command
        /// </summary>
        private ICommand _closeCommandCancel;

        /// <summary>
        /// Gets the 'CloseCommandOk' command
        /// </summary>
        public ICommand CloseCommandOk
        {
            get
            {
                if (_closeCommandOk == null)
                {
                    _closeCommandOk = new CommandBase((o) => _closeOk(o), null);
                }
                return _closeCommandOk;
            }
        }

        /// <summary>
        /// Gets the 'CloseCommandCancel' command
        /// </summary>
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

        /// <summary>
        /// 'ItemEditWindowViewModel' constructor
        /// </summary>
        /// <param name="taskWidget">Task widget object to edit</param>
        public ItemEditWindowViewModel(TaskWidget taskWidget)
        {
            this.SetTaskWidget(taskWidget);
        }

        /// <summary>
        /// Gets or sets content of the item
        /// </summary>
        public string ItemContent
        {
            get
            {
                return _itemContent;
            }
            set
            {
                _itemContent = value;
                this.OnNotifyPropertyChanged("ItemContent");
            }
        }

        /// <summary>
        /// Set the task widget object to edit
        /// </summary>
        /// <param name="taskWidget"></param>
        public void SetTaskWidget(TaskWidget taskWidget)
        {
            _taskWidget = taskWidget;
            _itemContent = _taskWidget.TaskContent;
        }

        /// <summary>
        /// Executes at the time of the 'btnCancel' clicked
        /// </summary>
        /// <param name="o">Sender object</param>
        public void _closeOk(object o)
        {
            _taskWidget.TaskContent = _itemContent;
            if (o is System.Windows.Window)
            {
                ((System.Windows.Window)o).DialogResult = true;
                ((System.Windows.Window)o).Close();
            }
        }
    }
}
