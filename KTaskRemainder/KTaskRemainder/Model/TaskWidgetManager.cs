using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTaskRemainder.Model
{
    public class TaskWidgetManager
    {
        /// <summary>
        /// Singleton for 'TaskWidgetManager' class
        /// </summary>
        private static TaskWidgetManager _instance;

        /// <summary>
        /// Stores the collections of 'TaskWidget' objects
        /// </summary>
        private Dictionary<string, ObservableCollection<TaskWidget>> _taskWidgetCollections;

        private TaskWidgetManager()
        {
            _taskWidgetCollections = new Dictionary<string, ObservableCollection<TaskWidget>>();
        }

        /// <summary>
        /// Return singleton for 'TaskWidgetManager' class
        /// </summary>
        public static TaskWidgetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskWidgetManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates a collection of 'TaskWidget' objects
        /// </summary>
        /// <param name="collectionName">Name for collection</param>
        /// <returns>Return 'true' if the object creation was succesfull</returns>
        public ObservableCollection<TaskWidget> CreateTaskWidgetCollection(string collectionName)
        {
            if (!_taskWidgetCollections.ContainsKey(collectionName))
            {
                ObservableCollection<TaskWidget> tmpCollection = new ObservableCollection<TaskWidget>();
                _taskWidgetCollections[collectionName] = tmpCollection;
                return tmpCollection;
            }
            return null;
        }

        /// <summary>
        /// Return collection of the 'TaskWidget' objects
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <returns> Return collection</returns>
        public ObservableCollection<TaskWidget> GetTaskWidgetCollection(string collectionName)
        {
            if (_taskWidgetCollections.ContainsKey(collectionName))
            {
                return _taskWidgetCollections[collectionName];
            }
            return null;
        }

        public bool RemoveFromCollection(TaskWidget widget, string collectionName = null)
        {
            if (widget != null)
            {
                if (String.IsNullOrWhiteSpace(collectionName))
                {
                    collectionName = this.FindCollectionName(widget);
                }
                if (!String.IsNullOrWhiteSpace(collectionName))
                {
                    ObservableCollection<TaskWidget> collection = this.GetTaskWidgetCollection(collectionName);
                    if (collectionName != null)
                    {
                        collection.Remove(widget);
                    }
                }
            }
            return false;
        }

        public bool AddToCollection(TaskWidget widget, string collectionName, int index = 0)
        {
            if (widget != null &&
                collectionName != null)
            {
                ObservableCollection<TaskWidget> collection = this.GetTaskWidgetCollection(collectionName);
                if (collection != null)
                {
                    if (index < 0)
                    {
                        index = 0;
                    }
                    else if (index >= collection.Count)
                    {
                        index = collection.Count;
                    }
                    collection.Insert(index, widget);
                }
            }
            return false;
        }

        public string FindCollectionName(TaskWidget widget)
        {
            Dictionary<string, ObservableCollection<TaskWidget>>.Enumerator e = _taskWidgetCollections.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value.Contains(widget))
                {
                    return e.Current.Key;
                }
            }
            return null;
        }

        public int GetTaskWidgetIndex(TaskWidget widget)
        {
            ObservableCollection<TaskWidget> collection = this.GetTaskWidgetCollection(widget.CollectionName);
            if (collection != null)
            {
                return collection.IndexOf(widget);
            }
            return -1;
        }
    }
}
