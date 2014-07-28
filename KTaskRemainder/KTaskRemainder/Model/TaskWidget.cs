using System;
using System.ComponentModel;
using KTaskRemainder.Common;
using KTaskRemainder.Behavior;
using KTaskRemainder.Database;

namespace KTaskRemainder.Model
{
    /// <summary>
    /// Representation of single TASK content
    /// </summary>
    public class TaskWidget : INotifyPropertyChanged, IEquatable<TaskWidget>
    {
        #region Private variables

        /// <summary>
        /// Task unique guid
        /// </summary>
        private Guid _taskGuid = Guid.Empty;

        /// <summary>
        /// Importance and urgency information
        /// </summary>
        private bool _important, _urgent;

        /// <summary>
        /// Task content
        /// </summary>
        private string _taskContent = String.Empty;

        /// <summary>
        /// Task priority
        /// </summary>
        private ETaskPriority _taskPriority = ETaskPriority.Middle;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the task priority (not implemented)
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
                if (_taskGuid != null &&
                    _taskGuid != Guid.Empty)
                {
                    DbManager.Update(_taskGuid, value, null, null);
                }
                this.OnNotifyPropertyChanged("TaskContent");
            }
        }

        /// <summary>
        /// Gets task unique guid
        /// </summary>
        public Guid TaskGuid
        {
            get { return _taskGuid; }
        }

        /// <summary>
        /// Gets task importance information
        /// </summary>
        public bool Important
        {
            get { return _important; }
            set
            {
                _important = value;
                if (_taskGuid != null &&
                    _taskGuid != Guid.Empty)
                {
                    DbManager.Update(_taskGuid, null, value, null);
                }
            }
        }

        /// <summary>
        /// Get task grgency information
        /// </summary>
        public bool Urgent
        {
            get { return _urgent; }
            set
            {
                _urgent = value;
                if (_taskGuid != null &&
                    _taskGuid != Guid.Empty)
                {
                    DbManager.Update(_taskGuid, null, null, value);
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the 'TaskWidget' class
        /// </summary>
        /// <param name="content">Content of the task item</param>
        /// <param name="important">If task is important</param>
        /// <param name="urgent">If task is urgent</param>
        public TaskWidget(string content, bool important, bool urgent) :
            this(content, important, urgent, Guid.NewGuid())
        { }

        /// <summary>
        /// Constructor of the 'TaskWidget' class
        /// </summary>
        /// <param name="content">Content of the task item</param>
        /// <param name="content">Content of the task item</param>
        /// <param name="important">If task is important</param>
        /// <param name="guid">Task unique guid</param>
        public TaskWidget(string content, bool important, bool urgent, Guid guid)
        {
            TaskContent = content;
            _important = important;
            _urgent = urgent;
            _taskGuid = guid;
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
