using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTaskRemainder.Database
{
    /// <summary>
    /// Common class for the databases handling
    /// </summary>
    public class DbCommon
    {
        #region Constant members

        /// <summary>
        /// Name of the table
        /// </summary>
        public const string TABLE_NAME = "Tasks";

        /// <summary>
        /// Name of the Guid column
        /// </summary>
        public const string GUIN_COLUMN = "Guid";

        /// <summary>
        /// Name of the Text column
        /// </summary>
        public const string TEXT_COLUMN = "Text";

        /// <summary>
        /// Name of the Important column
        /// </summary>
        public const string IMPORTANT_COLUMN = "Important";

        /// <summary>
        /// Name of the Urgent column
        /// </summary>
        public const string URGENT_COLUMN = "Urgent";

        #endregion

        #region Task class

        /// <summary>
        /// Class represents a single row in database
        /// </summary>
        public class Task
        {
            /// <summary>
            /// Contents of the Guid field
            /// </summary>
            private Guid _guid;

            /// <summary>
            /// Contents of the Text field
            /// </summary>
            private string _text;

            /// <summary>
            /// Contents of the Important field
            /// </summary>
            private bool _important;

            /// <summary>
            /// Contents of the Urgent field
            /// </summary>
            private bool _urgent;

            /// <summary>
            /// Gets contents of the Guid field
            /// </summary>
            public Guid Guid
            {
                get { return _guid; }
            }

            /// <summary>
            /// Gets contents of the Text field
            /// </summary>
            public string Text
            {
                get { return _text; }
            }

            /// <summary>
            /// Gets contents of the Important field
            /// </summary>
            public bool Important
            {
                get { return _important; }
            }

            /// <summary>
            /// Gets contents of the Urgent field
            /// </summary>
            public bool Urgent
            {
                get { return _urgent; }
            }

            /// <summary>
            /// 'Task' constructor
            /// </summary>
            /// <param name="guid">Contents of the Guid field</param>
            /// <param name="text">Contents of the Text field</param>
            /// <param name="important">Contents of the Important field</param>
            /// <param name="urgent">Contents of the Urgent field</param>
            public Task(string guid, string text, string important, string urgent)
            {
                if (Guid.TryParse(guid, out _guid))
                {
                    _text = text;
                    _important = important == "1" ? true : false;
                    _urgent = urgent == "1" ? true : false;
                }
            }
        }

        #endregion
    }
}
