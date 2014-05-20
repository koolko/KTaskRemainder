using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Controls;
using KTaskRemainder.ViewModel;
using KTaskRemainder.Model;

namespace KTaskRemainder.Behavior
{
    public class ListBoxDragDropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (this.AssociatedObject.DataContext != null &&
                this.AssociatedObject is ListBox &&
                this.AssociatedObject.DataContext is TaskWidgetsViewModel)
            {
                ListBox listBox = this.AssociatedObject as ListBox;
                if (listBox != null)
                {
                    int index = 0;
                    foreach (object item in listBox.Items)
                    {
                        ListBoxItem lbi = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                        if (lbi != null)
                        {
                            Point transform = lbi.TransformToVisual((Visual)listBox).Transform(new Point());
                            if (transform.Y + lbi.ActualHeight > e.GetPosition(this.AssociatedObject).Y)
                            {
                                break;
                            }
                            index++;
                        }
                    }
                    IDragDrop obj = e.Data.GetData(typeof(TaskWidget)) as IDragDrop;
                    obj.Remove();
                    obj.Drop(((TaskWidgetsViewModel)this.AssociatedObject.DataContext).CollectionName, index);
                }
            }
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            Point currentPosition = e.GetPosition(this.AssociatedObject);
            HitTestResult result = VisualTreeHelper.HitTest(this.AssociatedObject, currentPosition);
            if (result != null &&
                result.VisualHit != null &&
                result.VisualHit is FrameworkElement &&
                ((FrameworkElement)result.VisualHit).DataContext is KTaskRemainder.Model.TaskWidget)
            {
                Console.WriteLine(((KTaskRemainder.Model.TaskWidget)((FrameworkElement)result.VisualHit).DataContext).TaskContent);
            }
        }
    }
}
