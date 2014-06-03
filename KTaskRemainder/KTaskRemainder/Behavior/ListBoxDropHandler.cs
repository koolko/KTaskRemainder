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
            }
        }
    }
}
