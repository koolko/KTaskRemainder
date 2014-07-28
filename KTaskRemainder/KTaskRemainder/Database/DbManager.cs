using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTaskRemainder.Database
{
    /// <summary>
    /// Database manager
    /// </summary>
    public class DbManager
    {
        /// <summary>
        /// Database engine instance
        /// </summary>
        private static IDatabase _db;

        private const string _DB_DEFAULT_NAME = "default.db";

        /// <summary>
        /// Static constructor
        /// </summary>
        static DbManager()
        {
            _db = new DbSqlite();
            if (!_db.SetDbName(_DB_DEFAULT_NAME) ||
                !_db.OpenDb())                          // if db doesn't exists
            {
                DbManager.CreateDB(_DB_DEFAULT_NAME);   // create new one
            }
            else
            {
                _db.CloseDb();
            }
        }

        /// <summary>
        /// Create new database
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool CreateDB(string name)
        {
            return (_db.SetDbName(name) &&
                    _db.CreateDb());
        }

        /// <summary>
        /// Add record to database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content</param>
        /// <param name="important">If task is important</param>
        /// <param name="urgent">If task is urgent</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool Add(Guid guid, string content, bool important, bool urgent)
        {
            return _db.Add(guid, content, important, urgent);
        }

        /// <summary>
        /// Update database record
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content (null -> without change)</param>
        /// <param name="important">If task is important (null -> without change)</param>
        /// <param name="urgent">If task is urgent (null -> without change)</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool Update(Guid guid, string task = null, bool? important = null, bool? urgent = null)
        {
            return _db.Update(guid, task, important, urgent);
        }

        /// <summary>
        /// Remove record from database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool Remove(Guid guid)
        {
            return _db.Remove(guid);
        }

        /// <summary>
        /// Get all tasks from database
        /// </summary>
        /// <param name="tasks">Return result</param>
        /// <param name="important">If tasks are important</param>
        /// <param name="urgent">If tasks are urgent</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool GetAllTasks(out List<DbCommon.Task> tasks, bool? important = null, bool? urgent = null)
        {
            return _db.GetAllTasks(out tasks, important, urgent);
        }
    }
}
