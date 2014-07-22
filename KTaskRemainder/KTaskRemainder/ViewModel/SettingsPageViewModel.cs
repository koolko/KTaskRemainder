using System;
using System.Windows.Input;
using KTaskRemainder.Common;
using KTaskRemainder.Database;

namespace KTaskRemainder.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private ICommand _createDbCommand;

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
