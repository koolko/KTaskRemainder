using System;
using KTaskRemainder.ViewModel;

namespace KTaskRemainder.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page
    {
        public MainPage()
        {
            InitializeComponent();

            TaskWidgetsViewModel _viewModelFirst = new TaskWidgetsViewModel(listViewFirst.Name);
            _viewModelFirst.TaskWidgets.Add(new Model.TaskWidget("pierwszy"));
            _viewModelFirst.TaskWidgets.Add(new Model.TaskWidget("drugi"));
            listViewFirst.DataContext = _viewModelFirst;

            TaskWidgetsViewModel _viewModelSecond = new TaskWidgetsViewModel(listViewSecond.Name);
            _viewModelSecond.TaskWidgets.Add(new Model.TaskWidget("trzeci"));
            _viewModelSecond.TaskWidgets.Add(new Model.TaskWidget("czwarty"));
            listViewSecond.DataContext = _viewModelSecond;

            TaskWidgetsViewModel _viewModelThird = new TaskWidgetsViewModel(listViewThird.Name);
            listViewThird.DataContext = _viewModelThird;

            TaskWidgetsViewModel _viewModelFourth = new TaskWidgetsViewModel(listViewFourth.Name);
            listViewFourth.DataContext = _viewModelFourth;
        }
    }
}
