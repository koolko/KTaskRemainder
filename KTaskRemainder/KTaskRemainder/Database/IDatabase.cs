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
        /// Create new database
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool CreateDb(string name);

        /// <summary>
        /// Delete database
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool DeleteDb(string name);

        /// <summary>
        /// Open database
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        bool OpenDb(string name);

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
    }
}
