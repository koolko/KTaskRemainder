using System;
using GongSolutions.Wpf.DragDrop;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTaskRemainder.Behavior
{
    public class ListBoxDragHandler : IDragSource
    {
        public bool CanStartDrag(IDragInfo dragInfo)
        {
            return DragDrop.DefaultDragHandler.CanStartDrag(dragInfo);
        }

        public void DragCancelled()
        {
        }

        public void Dropped(IDropInfo dropInfo)
        {

        }

        public void StartDrag(IDragInfo dragInfo)
        {
            DragDrop.DefaultDragHandler.StartDrag(dragInfo);
        }
    }
}
