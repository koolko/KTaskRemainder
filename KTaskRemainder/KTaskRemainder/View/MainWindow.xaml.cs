using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KTaskRemainder.ViewModel;

namespace KTaskRemainder.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
        }
    }
}
