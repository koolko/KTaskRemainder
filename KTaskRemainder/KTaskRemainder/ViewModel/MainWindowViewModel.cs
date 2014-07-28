using System;
using System.Windows.Input;
using System.Windows.Navigation;
using KTaskRemainder.Common;
using KTaskRemainder.View;

namespace KTaskRemainder.ViewModel
{
    /// <summary>
    /// ViewModel class for 'MainWindow'
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// 'SettingsPage' page
        /// </summary>
        private SettingsPage _settingsPage;

        /// <summary>
        /// 'MainPage' page
        /// </summary>
        private MainPage _mainPage;

        /// <summary>
        /// 'true' --> 'MainPage'; 'false' --> 'SettingsPage'
        /// </summary>
        private bool _bMain;

        /// <summary>
        /// 'SettingsCommand' command
        /// </summary>
        private ICommand _settingsCommand;

        /// <summary>
        /// Gets the 'SettingsCommand' command
        /// Navigate to settings page
        /// </summary>
        public ICommand SettingsCommand
        {
            get
            {
                
                if (_settingsCommand == null)
                {
                    _settingsCommand = new CommandBase((o) =>
                                                                {
                                                                    if (_bMain)
                                                                    {
                                                                        _mainPage.NavigationService.Navigate(_settingsPage);
                                                                        _bMain = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        _settingsPage.NavigationService.GoBack();
                                                                        _bMain = true;
                                                                    }
                                                                }, null);
                }
                return _settingsCommand;
            }
        }

        /// <summary>
        /// 'MainWindowViewModel' constructor
        /// </summary>
        /// <param name="mainPage"></param>
        public MainWindowViewModel(MainPage mainPage)
        {
            _settingsPage = new SettingsPage();
            _mainPage = mainPage;
            _bMain = true;
        }
    }
}
