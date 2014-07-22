using System;
using System.Windows.Input;
using System.Windows.Navigation;
using KTaskRemainder.Common;
using KTaskRemainder.View;

namespace KTaskRemainder.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private SettingsPage _settingsPage;
        private MainPage _mainPage;
        private bool _bMain;

        private ICommand _settingsCommand;

        public MainWindowViewModel(MainPage mainPage)
        {
            _settingsPage = new SettingsPage();
            _mainPage = mainPage;
            _bMain = true;
        }

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
    }
}
