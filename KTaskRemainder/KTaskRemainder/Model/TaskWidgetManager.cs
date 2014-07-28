using KTaskRemainder.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTaskRemainder.Model
{
    /// <summary>
    /// Manager of the 'TaskWidget' items
    /// </summary>
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

        /// <summary>
        /// 'TaskWidgetManager' constructor
        /// </summary>
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

        /// <summary>
        /// Remove 'TaskWidget' object from 'TaskWidget' collection
        /// </summary>
        /// <param name="widget">'TaskWidget' object</param>
        /// <param name="collectionName">'TaskWidget' collection</param>
        /// <returns>Return 'true' if the object removel was succesfull</returns>
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
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Add 'TaskWidget' object to 'TaskWidget' collection
        /// </summary>
        /// <param name="widget">'TaskWidget' object</param>
        /// <param name="collectionName">'TaskWidget' collection</param>
        /// <param name="index">Index of the 'TaskWidget' object</param>
        /// <returns>Return 'true' if the object addition was succesfull</returns>
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

        /// <summary>
        /// Find collection name based on 'TaskWidget' object
        /// </summary>
        /// <param name="widget">'TaskWidget' object</param>
        /// <returns>Returns collection name (null if collection name doesn't exist)</returns>
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

        /// <summary>
        /// Find item index based on 'TaskWidget' object
        /// </summary>
        /// <param name="widget">'TaskWidget' object</param>
        /// <returns>Returns item index (-1 if 'TaskWidget' object exist and belongs to collection)</returns>
        public int GetTaskWidgetIndex(TaskWidget widget)
        {
            if (widget != null)
            {
                ObservableCollection<TaskWidget> collection = this.GetTaskWidgetCollection(widget.CollectionName);
                if (collection != null)
                {
                    return collection.IndexOf(widget);
                }
            }
            return -1;
        }
    }
}
