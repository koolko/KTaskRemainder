using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace KTaskRemainder.Database
{
    /// <summary>
    /// SQLite database engine
    /// </summary>
    public class DbSqlite : IDatabase
    {
        /// <summary>
        /// SQLite connection
        /// </summary>
        private SQLiteConnection _dbConnection;

        /// <summary>
        /// Database name
        /// </summary>
        private string _name;

        /// <summary>
        /// Set database name
        /// </summary>
        /// <param name="name">Database name</param>
        public bool SetDbName(string name)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(name))
            {
                _name = name;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Create new database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool CreateDb()
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(_name))
            {
                try
                {
                    if (System.IO.File.Exists(_name))
                    {
                        this.DeleteDb();
                    }
                    SQLiteConnection.CreateFile(_name);

                    int rowsUpdated = 0;
                    result = this._ExecuteNonQuery(out rowsUpdated, String.Format("CREATE TABLE [{0}] (" +
                                                                                 "[{1}] TEXT NULL," +
                                                                                 "[{2}] TEXT NULL," +
                                                                                 "[{3}] BOOLEAN default 0," +
                                                                                 "[{4}] BOOLEAN default 0)",
                                                                                 DbCommon.TABLE_NAME,       // {0}
                                                                                 DbCommon.GUIN_COLUMN,      // {1}
                                                                                 DbCommon.TEXT_COLUMN,      // {2}
                                                                                 DbCommon.IMPORTANT_COLUMN, // {3}
                                                                                 DbCommon.URGENT_COLUMN));  // {4}
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Delete existing database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool DeleteDb()
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(_name))
            {
                this.CloseDb();
                if (System.IO.File.Exists(_name))
                {
                    System.IO.File.Delete(_name);
                }
                result = !System.IO.File.Exists(_name);
            }
            return result;
        }

        /// <summary>
        /// Open database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool OpenDb()
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(_name))
            {
                _dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;",
                                                                   _name));
                _dbConnection.Open();
                SQLiteErrorCode code = _dbConnection.ResultCode();
                result = (code == SQLiteErrorCode.Ok);
            }
            return result;
        }

        /// <summary>
        /// Close database
        /// </summary>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool CloseDb()
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(_name) &&
                _dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Add record to database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content</param>
        /// <param name="important">If task is important</param>
        /// <param name="urgent">If task is urgent</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool Add(Guid guid, string task, bool important = true, bool urgent = true)
        {
            bool result = true;
            try
            {
                int rowsUpdated = 0;
                result = this._ExecuteNonQuery(out rowsUpdated, String.Format("INSERT INTO {0}({1},{2},{3},{4}) VALUES('{5}','{6}','{7}','{8}')",
                                                                             DbCommon.TABLE_NAME,       // {0}
                                                                             DbCommon.GUIN_COLUMN,      // {1}
                                                                             DbCommon.TEXT_COLUMN,      // {2}
                                                                             DbCommon.IMPORTANT_COLUMN, // {3}
                                                                             DbCommon.URGENT_COLUMN,    // {4}
                                                                             guid.ToString(),           // {5}
                                                                             task,                      // {6}
                                                                             important ? "1" : "0",     // {7}
                                                                             urgent ? "1" : "0"));      // {8}
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Remove record from database
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool Remove(Guid guid)
        {
            bool result = true;
            try
            {
                int rowsUpdated = 0;
                result = this._ExecuteNonQuery(out rowsUpdated, String.Format("DELETE FROM {0} WHERE {1}='{2}'",
                                                                             DbCommon.TABLE_NAME,   // {0}
                                                                             DbCommon.GUIN_COLUMN,  // {1}
                                                                             guid.ToString()));     // {2}
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Update database record
        /// </summary>
        /// <param name="guid">Task unique guid</param>
        /// <param name="task">Task content</param>
        /// <param name="important">If task is important (null -> without change)</param>
        /// <param name="urgent">If task is urgent (null -> without change)</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool Update(Guid guid, string task = null, bool? important = null, bool? urgent = null)
        {
            bool result = false;
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                if (task != null ||
                    important != null ||
                    urgent != null)
                {
                    string set = String.Empty;
                    if (task != null)
                    {
                        set = String.Format("{0}='{1}'",
                                            DbCommon.TEXT_COLUMN,   // {0}
                                            task);                  // {1}
                    }
                    if (important != null)
                    {
                        if (!String.IsNullOrWhiteSpace(set))
                        {
                            set = String.Format("{0},", set);
                        }
                        set = String.Format("{0}{1}",
                                            set,                                                    // {0}
                                            String.Format("{0}='{1}'",                              // {1}
                                                          DbCommon.IMPORTANT_COLUMN,        // {0}
                                                          important.Value ? "1" : "0"));    // {1}
                    }
                    if (urgent != null)
                    {
                        if (!String.IsNullOrWhiteSpace(set))
                        {
                            set = String.Format("{0},", set);
                        }
                        set = String.Format("{0}{1}",
                                            set,                                                // {0}
                                            String.Format("{0}='{1}'",                          // {1}
                                                          DbCommon.URGENT_COLUMN,       // {0}
                                                          urgent.Value ? "1" : "0"));   // {1}
                    }
                    int rowsUpdated = 0;
                    result = this._ExecuteNonQuery(out rowsUpdated, String.Format("UPDATE {0} SET {1} WHERE {2}='{3}'",
                                                                                  DbCommon.TABLE_NAME,   // {0}
                                                                                  set,                   // {1}
                                                                                  DbCommon.GUIN_COLUMN,  // {2}
                                                                                  guid.ToString()));     // {3}
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Get tasks from database
        /// </summary>
        /// <param name="important">If tasks are important</param>
        /// <param name="urgent">If tasks are urgent</param>
        /// <param name="tasks">Return result</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool GetAllTasks(out List<DbCommon.Task> tasks, bool? important = null, bool? urgent = null)
        {
            bool result = false;
            tasks = new List<DbCommon.Task>();
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                if (this.OpenDb())
                {
                    string where = String.Empty;
                    if (important != null)
                    {
                        where =  String.Format(" WHERE {0}='{1}'",
                                               DbCommon.IMPORTANT_COLUMN,   // {0}
                                               important.Value ? "1" : "0");// {1}
                    }
                    if (urgent != null)
                    {
                        if (!String.IsNullOrWhiteSpace(where))
                        {
                            where = String.Format("{0} AND ",
                                                  where);
                        }
                        else
                        {
                            where = " WHERE ";
                        }
                        where = String.Format("{0}{1}='{2}'",
                                              where,                    // {0}
                                              DbCommon.URGENT_COLUMN,   // {1}
                                              urgent.Value ? "1" : "0");// {2}
                    }

                    SQLiteCommand myCommand = new SQLiteCommand(_dbConnection);
                    myCommand.CommandText = String.Format("SELECT {0},{1},{2},{3} FROM {4}{5}",
                                                          DbCommon.GUIN_COLUMN,         // {0}
                                                          DbCommon.TEXT_COLUMN,         // {1}
                                                          DbCommon.IMPORTANT_COLUMN,    // {2}
                                                          DbCommon.URGENT_COLUMN,       // {3}
                                                          DbCommon.TABLE_NAME,          // {4}
                                                          where);                       // {5}
                    SQLiteDataReader reader = myCommand.ExecuteReader();

                    dt.Load(reader);
                    reader.Close();
                    result = _dbConnection.ResultCode() == SQLiteErrorCode.Ok;
                    this.CloseDb();

                    IEnumerator<System.Data.DataRow> enumerator = (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();
                    while(enumerator.MoveNext())
                    {
                        if (enumerator.Current.ItemArray.Count() > 3)
                        {
                            DbCommon.Task task = new DbCommon.Task(enumerator.Current.ItemArray[0].ToString(),
                                                                   enumerator.Current.ItemArray[1].ToString(),
                                                                   enumerator.Current.ItemArray[2].ToString(),
                                                                   enumerator.Current.ItemArray[3].ToString());
                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Get task from database
        /// </summary>
        /// <param name="task">Return result</param>
        /// <param name="guid">Task unique guid</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        public bool GetTask(out DbCommon.Task task, Guid guid)
        {
            bool result = false;
            task = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                if (this.OpenDb() &&
                    guid != null)
                {
                    SQLiteCommand myCommand = new SQLiteCommand(_dbConnection);
                    myCommand.CommandText = String.Format("SELECT {0},{1},{2},{3} FROM {4} WHERE {5}='{6}'",
                                                          DbCommon.GUIN_COLUMN,         // {0}
                                                          DbCommon.TEXT_COLUMN,         // {1}
                                                          DbCommon.IMPORTANT_COLUMN,    // {2}
                                                          DbCommon.URGENT_COLUMN,       // {3}
                                                          DbCommon.TABLE_NAME,          // {4}
                                                          DbCommon.GUIN_COLUMN,         // {5}
                                                          guid.ToString());             // {6}
                    SQLiteDataReader reader = myCommand.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    result = _dbConnection.ResultCode() == SQLiteErrorCode.Ok;
                    this.CloseDb();
                    if (dt.Rows.Count > 0 &&
                        dt.Rows[0].ItemArray.Count() > 3)
                    {
                        task = new DbCommon.Task(dt.Rows[0].ItemArray[0].ToString(),
                                                 dt.Rows[0].ItemArray[1].ToString(),
                                                 dt.Rows[0].ItemArray[2].ToString(),
                                                 dt.Rows[0].ItemArray[3].ToString());
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Execution of the query
        /// </summary>
        /// <param name="rowsUpdated">Amount of rows updated</param>
        /// <param name="sql">SQL query</param>
        /// <returns>Returns 'true' if operation was successful</returns>
        private bool _ExecuteNonQuery(out int rowsUpdated, string sql)
        {
            bool result = false;
            rowsUpdated = 0;
            if (!String.IsNullOrWhiteSpace(sql) &&
                this.OpenDb())
            {
                SQLiteCommand myCommand = new SQLiteCommand(_dbConnection);
                myCommand.CommandText = sql;
                rowsUpdated = myCommand.ExecuteNonQuery();
                result = _dbConnection.ResultCode() == SQLiteErrorCode.Ok;
                this.CloseDb();
            }
            return result;
        }
    }
}
