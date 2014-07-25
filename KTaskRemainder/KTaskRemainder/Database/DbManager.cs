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

        /// <summary>
        /// Static constructor
        /// </summary>
        static DbManager()
        {
            _db = new DbSqlite();
        }

        /// <summary>
        /// Create new database
        /// </summary>
        /// <param name="name">Database name</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public static bool CreateDB(string name)
        {
            return (_db.SetDbName(name) && _db.CreateDb());
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
    }
}
