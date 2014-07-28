using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GongSolutions.Wpf.DragDrop;
using KTaskRemainder.Model;
using System.Collections.ObjectModel;

namespace KTaskRemainder.Behavior
{
    public class ListBoxDropHandler : IDropTarget
    {
        public void DragOver(IDropInfo dropInfo)
        {
            DragDrop.DefaultDropHandler.DragOver(dropInfo);
        }

        public void Drop(IDropInfo dropInfo)
        {
            DragDrop.DefaultDropHandler.Drop(dropInfo);
            if (dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection)
            {
                TaskWidget tw = dropInfo.DragInfo.SourceItem as TaskWidget;
                if (tw != null)
                {
                    ObservableCollection<TaskWidget> collection = dropInfo.DragInfo.SourceCollection as ObservableCollection<TaskWidget>;
                    if (collection != null)
                    {
                        collection.Remove(tw);
                    }
                }
                Console.WriteLine(((System.Windows.Controls.ListBox)dropInfo.VisualTarget).Tag);

                System.Windows.DependencyObject parent = dropInfo.VisualTarget;
                bool ok = false;
                while (!ok &&
                        parent != null)
                {
                    parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
                    if (parent is System.Windows.Controls.UserControl)
                    {
                        ok = true;
                    }
                }
                if (ok)
                {
                    bool important = ((string)((System.Windows.Controls.UserControl)parent).Tag == "1" || (string)((System.Windows.Controls.UserControl)parent).Tag == "3");
                    bool urgent = ((string)((System.Windows.Controls.UserControl)parent).Tag == "1" || (string)((System.Windows.Controls.UserControl)parent).Tag == "2");

                    tw.Important = important;
                    tw.Urgent = urgent;
                }
            }
        }
    }
}
