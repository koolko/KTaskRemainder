using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace KTaskRemainder.Behavior
{
    public class ListBoxDragDropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
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
