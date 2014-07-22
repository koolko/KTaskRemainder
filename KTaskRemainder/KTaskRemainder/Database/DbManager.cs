using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTaskRemainder.Database
{
    public class DbManager
    {
        private static IDatabase _db;

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
            return _db.CreateDb(name);
        }
    }
}
