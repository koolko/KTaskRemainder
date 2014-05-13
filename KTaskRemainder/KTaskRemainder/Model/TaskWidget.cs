using System;
using System.ComponentModel;
using KTaskRemainder.Common;
using KTaskRemainder.Behavior;

namespace KTaskRemainder.Model
{
    /// <summary>
    /// Representation of single TASK content
    /// </summary>
    public class TaskWidget : IDragDrop, INotifyPropertyChanged, IEquatable<TaskWidget>
    {
        #region Private variables

        private Guid _taskGuid = Guid.Empty;
        private string _collectionName = null;
        private string _taskContent = String.Empty;
        private ETaskPriority _taskPriority = ETaskPriority.Middle;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the task priority
        /// </summary>
        public ETaskPriority TaskPriority
        {
            get { return _taskPriority; }
            set
            {
                _taskPriority = value;
                this.OnNotifyPropertyChanged("TaskPriority");
            }
        }

        /// <summary>
        /// Gets or sets content of the task item
        /// </summary>
        public string TaskContent
        {
            get { return _taskContent; }
            set
            {
                _taskContent = value;
                this.OnNotifyPropertyChanged("TaskContent");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the 'TaskWidget' class
        /// </summary>
        /// <param name="content">Content of the task item</param>
        public TaskWidget(string content)
        {
            _taskGuid = Guid.NewGuid();
            TaskContent = content;
        }

        #endregion

        #region IDragDrop implementation

        /// <summary>
        /// Returns the collection to which the object belongs
        /// </summary>
        public string CollectionName
        {
            get
            {
                return TaskWidgetManager.Instance.FindCollectionName(this);
            }
        }

        /// <summary>
        /// Index of the object in his collection
        /// </summary>
        public int Index
        {
            get
            {
                return TaskWidgetManager.Instance.GetTaskWidgetIndex(this);
            }
        }

        /// <summary>
        /// Remove object from collection
        /// </summary>
        public void Remove()
        {
            TaskWidgetManager.Instance.RemoveFromCollection(this, CollectionName);
        }

        /// <summary>
        /// Drop object to collection
        /// </summary>
        /// <param name="collection">Collection in which the object should be placed</param>
        /// <param name="index">Index location of the dropped object</param>
        public void Drop(string collection, int index = -1)
        {
            if (!String.IsNullOrWhiteSpace(collection))
            {
                TaskWidgetManager.Instance.AddToCollection(this, collection, index);
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IEquatable implementation

        public bool Equals(TaskWidget other)
        {
            return (this._taskGuid.Equals(other._taskGuid));
        }

        #endregion
    }
}
