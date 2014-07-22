using System;
using System.Windows.Controls;

namespace KTaskRemainder.View
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.SettingsPageViewModel();
        }
    }
}
