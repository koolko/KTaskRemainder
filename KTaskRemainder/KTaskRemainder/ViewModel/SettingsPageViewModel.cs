using System;
using System.Windows.Input;
using KTaskRemainder.Common;
using KTaskRemainder.Database;

namespace KTaskRemainder.ViewModel
{
    /// <summary>
    /// ViewModel class for 'SettingsPage'
    /// </summary>
    public class SettingsPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 'CreateDbCommand' command
        /// </summary>
        private ICommand _createDbCommand;

        /// <summary>
        /// Gets the 'CreateDbCommand' command
        /// </summary>
        public ICommand CreateDbCommand
        {
            get
            {
                if (_createDbCommand == null)
                {
                    _createDbCommand = new CommandBase((o) => { DbManager.CreateDB("test.db"); });
                }
                return _createDbCommand;
            }
        }
    }
}
