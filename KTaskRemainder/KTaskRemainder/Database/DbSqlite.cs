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
        private SQLiteConnection _dbConnection;

        public bool CreateDb(string name)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(name))
            {
                if (System.IO.File.Exists(name))
                {
                    this.DeleteDb(name);
                }
                SQLiteConnection.CreateFile(name);
                result = true;
            }
            return result;
        }

        public bool DeleteDb(string name)
        {
            System.IO.File.Delete(name);
            return !System.IO.File.Exists(name);
        }

        public bool OpenDb(string name)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(name))
            {
                _dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", name));
                _dbConnection.Open();
                SQLiteErrorCode code = _dbConnection.ResultCode();
                result = (code == SQLiteErrorCode.Done);
            }
            return result;
        }

        public bool Add(Guid guid, string task, bool important = true, bool urgent = true)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid guid)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid guid, string task = null, bool? important = null, bool? urgent = null)
        {
            throw new NotImplementedException();
        }
    }
}
