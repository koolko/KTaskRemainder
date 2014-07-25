using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTaskRemainder.Database
{
    /// <summary>
    /// Database interface
    /// </summary>
    interface IDatabase
    {
        /// <summary>
        /// Set database name
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool SetDbName(string name);

        /// <summary>
        /// Create new database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool CreateDb();

        /// <summary>
        /// Delete existing database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool DeleteDb();

        /// <summary>
        /// Open database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool OpenDb();

        /// <summary>
        /// Close database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool CloseDb();

        /// <summary>
        /// Add record to database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content</param>
        /// <param name="important">If task is important</param>
        /// <param name="urgent">If task is urgent</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool Add(System.Guid guid, string task, bool important = true, bool urgent = true);

        /// <summary>
        /// Remove record from database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool Remove(System.Guid guid);

        /// <summary>
        /// Update database record
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content</param>
        /// <param name="important">If task is important (null -> without change)</param>
        /// <param name="urgent">If task is urgent (null -> without change)</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool Update(System.Guid guid, string task = null, bool? important = null, bool? urgent = null);

        /// <summary>
        /// Get all tasks from database
        /// </summary>
        /// <param name="tasks">Return result</param>
        /// <param name="important">If tasks are important</param>
        /// <param name="urgent">If tasks are urgent</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool GetAllTasks(out List<DbCommon.Task> tasks, bool? important = null, bool? urgent = null);

        /// <summary>
        /// Get task from database
        /// </summary>
        /// <param name="task">Return result</param>
        /// <param name="guid">Task unique guid</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool GetTask(out DbCommon.Task task, Guid guid);
    }
}
