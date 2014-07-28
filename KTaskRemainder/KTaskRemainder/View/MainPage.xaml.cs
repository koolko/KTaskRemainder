using System;
using KTaskRemainder.ViewModel;

namespace KTaskRemainder.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page
    {
        /// <summary>
        /// 'MainPage' constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            TaskWidgetsViewModel _viewModelFirst = new TaskWidgetsViewModel(listViewFirst.Name);
            listViewFirst.DataContext = _viewModelFirst;

            TaskWidgetsViewModel _viewModelSecond = new TaskWidgetsViewModel(listViewSecond.Name);
            listViewSecond.DataContext = _viewModelSecond;

            TaskWidgetsViewModel _viewModelThird = new TaskWidgetsViewModel(listViewThird.Name);
            listViewThird.DataContext = _viewModelThird;

            TaskWidgetsViewModel _viewModelFourth = new TaskWidgetsViewModel(listViewFourth.Name);
            listViewFourth.DataContext = _viewModelFourth;
        }
    }
}
