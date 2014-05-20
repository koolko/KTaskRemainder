using KTaskRemainder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace KTaskRemainder.Behavior
{
    public class ItemDragDropBehavior : Behavior<FrameworkElement>
    {
        private Thickness _defaultMargin;

        protected override void OnAttached()
        {
            base.OnAttached();

            _defaultMargin = this.AssociatedObject.Margin;

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            this.AssociatedObject.Drop += AssociatedObject_Drop;

            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                IDragDrop objSender = this.AssociatedObject.DataContext as IDragDrop;
                if (objSender != null &&
                    this.AssociatedObject.DataContext is TaskWidget)
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
            }
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            Point pos = e.GetPosition(this.AssociatedObject);
            if (pos.Y < this.AssociatedObject.ActualHeight / 2)
            {
                Console.WriteLine("góra");
                //this.AssociatedObject.Margin = new Thickness(this.AssociatedObject.Margin.Left, this.AssociatedObject.Margin.Top + 20, this.AssociatedObject.Margin.Right, this.AssociatedObject.Margin.Bottom);
            }
            else
            {
                Console.WriteLine("dół");
            }
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            this.AssociatedObject.Margin = _defaultMargin;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                IDragDrop obj = e.Data.GetData(typeof(TaskWidget)) as IDragDrop;
                IDragDrop objSender = this.AssociatedObject.DataContext as IDragDrop;
                if (obj != null &&
                    objSender != null &&
                    !obj.Equals(objSender))
                {
                    int index = objSender.Index;
                    Point pos = e.GetPosition(this.AssociatedObject);
                    if (pos.Y > this.AssociatedObject.ActualHeight / 2)
                    {
                        index += 1;
                    }
                    obj.Remove();
                    obj.Drop(objSender.CollectionName, index);
                }
            }
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.DragDrop.DoDragDrop(this.AssociatedObject, this.AssociatedObject.DataContext, DragDropEffects.Move);
            }
        }
    }
}
