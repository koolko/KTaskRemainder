using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using KTaskRemainder.Model;
using System.Windows.Controls;
using System.Windows.Media;

namespace KTaskRemainder.Behavior
{
    public class ListBoxItemBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.AssociatedObject.DataContext is TaskWidget)
            {
                if (e.ClickCount == 2)
                {
                    var itemWnd = new View.ItemEditWindow();
                    ViewModel.ItemEditWindowViewModel itemEditWindowViewModel =
                        new ViewModel.ItemEditWindowViewModel(itemWnd, (TaskWidget)this.AssociatedObject.DataContext);
                    itemWnd.DataContext = itemEditWindowViewModel;

                    string oldTxt = ((TaskWidget)this.AssociatedObject.DataContext).TaskContent;
                    if (itemWnd.ShowDialog() != true)
                    {
                        ((TaskWidget)this.AssociatedObject.DataContext).TaskContent = oldTxt;
                    }
                }
                else
                {
                    DependencyObject parent = this.AssociatedObject;
                    bool ok = false;
                    while (!ok && parent != null)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        if (parent is ListBox)
                        {
                            ok = true;
                        }
                    }
                    if (parent != null)
                    {
                        var viewModel = ((ListBox)parent).DataContext as ViewModel.TaskWidgetsViewModel;
                        if (viewModel != null)
                        {
                            viewModel.CurrentTask = this.AssociatedObject.DataContext as TaskWidget;
                        }
                    }
                }
            }
        }
    }
}
